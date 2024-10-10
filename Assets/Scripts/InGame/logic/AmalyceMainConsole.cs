using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AmalyceMainConsole : MonoBehaviour
{
    // Start is called before the first frame update
    public static InstanceCreatedObjects analyceInstance(string[]vectorText){
        string varName;
        int indexLine=0;
        int index=0;
        string [] vectorLine = vectorText[index].Split(' ');
       
        if(PreparationMng.clasesCreatedList.Any(obj=>obj.classVar.className==vectorLine[indexLine])){
            CreatedObjects copyObject = ((from obj in PreparationMng.clasesCreatedList 
                                where obj.classVar.className.Equals(vectorLine[indexLine], StringComparison.OrdinalIgnoreCase)
                                select obj).FirstOrDefault()).copy();
            /*foreach (var a in copyObject.classVar.constructor){
                Debug.Log(a);
            }*/
            indexLine++;
            varName=vectorLine[indexLine];
            indexLine++;

            if(vectorLine[indexLine]==";"){
                index++;
                //////////////////////////////////////////////////////////////////////
                ///PRUEBA DE OBJECTO
                ////////////////////////////
                Debug.Log("constructos de la clase");
                
                
                //throw new Exception ($"ERROR: falta modulo cuando no hay constructor");
                OClassIntance instanced = instanceEachOne(copyObject.classVar  ,vectorText,index,varName);
                return new InstanceCreatedObjects(instanced,vectorText,varName);

            }else if(vectorLine[indexLine]=="="){
                indexLine++;
                OClassIntance instanced = usingNew(vectorLine, copyObject.classVar,indexLine,copyObject.classVar.constructor);

                return new InstanceCreatedObjects(instanced,vectorText,varName);
                

            }else{
            throw new Exception("ERROR: problemas en la sintaxis despues de "+ vectorLine[indexLine-1]);
            }
        }else{

            throw new Exception ($"ERROR: La clase {vectorLine[indexLine]} no existe");
        }
        //Debug.Log(copyObject.classVar.className+" si encontroooo");
    }
    private static OClassIntance instanceEachOne(OClassIntance instanced, string [] textVector, int index , string classVarName){
        for(int i=index; i< textVector.Length;i++){
            int indexLine=0;
            OVarInstance variable;
            string[] textLine = textVector[i].Split(' ');
            if(textLine[indexLine]!=classVarName){
                throw new Exception ($"ERROR: {textLine[indexLine]} variable no instanciada en esta consola ");

            }else{
                indexLine++;
            }
            if(textLine[indexLine]!="."){
                throw new Exception ($"ERROR: Se esperaba . luego de {textLine[indexLine-1]}");

            }else{
                indexLine++;
            }
            if(instanced.atributes.Any(obj=>obj.varName==textLine[indexLine])){
                variable= ((from obj in instanced.atributes where obj.varName==textLine[indexLine] select obj).First()).copy();
                indexLine++;
                


            }else{
                throw new Exception ($"ERROR: {textLine[indexLine]} no es un atributo de la clase");  
            }
            if(textLine[indexLine]=="="){
                indexLine++;
            }else{
                throw new Exception ($"ERROR: despues de {textLine[indexLine-1]} se esperaba =");
            }
            verifyVariableType(variable.varType,textLine[indexLine]);
            instanced=assignValue(textLine[indexLine],instanced,variable.varName);
            indexLine++;
            if(textLine[indexLine]==";"){
                indexLine++;
            }else{
                throw new Exception ($"ERROR: despues de {textLine[indexLine-1]} se esperaba ;");
            }


        }
        return instanced;


    }
    private static OClassIntance usingNew(string[] textLine, OClassIntance instanced, int indexLine, string[] constructorVariables){
        if(textLine[indexLine]!="new"){
            throw new Exception ($"ERROR: despues de {textLine[indexLine-1]} se espera new");

        }else{
            indexLine++;

        }

        if(textLine[indexLine]!=instanced.className){
            throw new Exception ($"ERROR: despues de {textLine[indexLine-1]} se espera el mismo nombre de la clase");
            

        }else{

            indexLine++;
        }
        if(textLine[indexLine]!="("){
            throw new Exception ($"ERROR: despues de {textLine[indexLine-1]} se espera (");
            

        }else{

            indexLine++;
        }
        string [] constrVariables = instanced.constructor;

        for(int i=0;i<constrVariables.Length;i++){
            OVarInstance variable = ((from obj in instanced.atributes where obj.varName== constructorVariables[i].Replace("_","") select obj ).First()).copy();
            verifyVariableType(variable.varType,textLine[indexLine]);
            instanced=assignValue(textLine[indexLine],instanced,variable.varName);
            indexLine++;
            if(i<constructorVariables.Length-1){
                if(textLine[indexLine]==","){
                    indexLine++;

                }else{
                    throw new Exception($"ERROR: se esperaba ; luego de {textLine[indexLine]}"); 
                }


            }
            ////////////
            ///HASTA ACA
               

        }

        if(textLine[indexLine]!=")"){
            throw new Exception ($"ERROR: se esperaba ) luego de {textLine[indexLine]}");

        }else{
            indexLine++;
        }
        if(textLine[indexLine]!=";"){
            throw new Exception ($"ERROR: se esperaba ; luego de {textLine[indexLine]}");

        }else{
            indexLine++;
        }

        
        return instanced;



    }

    private static void verifyVariableType(string varType, string value){
        //Debug.Log("El parametro es "+ varType);
        switch(varType){
            case "int":
            Debug.Log("El parametro es int");
            try{
                
                Convert.ToInt32(value);

            }catch(Exception e){
                
                throw new Exception($"no coincide el valor con el tipo de variable {varType}");

            }
            break;

            case "bool":
            if(value!="true" && value!="false"){
                //Debug.Log("El parametro es bool");
                throw new Exception($"no coincide el valor con el tipo de variable {varType}");
                

            }
            break;
            case "string":
            Debug.Log("El parametro es string");

            if(value[0]=='"'&&value[value.Length-1]=='"'){
                //Debug.Log("El parametro es string");

            }else{

                throw new Exception ($"no coincide el valor con el tipo de variable {varType} el valor {value}");
            }
            break;


        }

    }

    private static OClassIntance assignValue (string value, OClassIntance instanced, string varName){

        foreach(var a in instanced.atributes){
            if(a.varName==varName){
                a.varValue=value;
                return instanced;

            }

        }

        throw new Exception ("ERROR INTERNO: No se encontro la variable en la clase");

        

    }

    public static (string, OClassIntance) analyceBattleConsole(string[] textvector){
        string [] textLine = textvector[0].Split(' ');
        int index=0;
        string action;


        if(PreparationMng.mainClassInstancedList.Any(obj=>obj.varName==textLine[index])){

            OClassIntance classTemp = (from obj in PreparationMng.mainClassInstancedList where obj.varName==textLine[index] select obj.classVar ).First();
            index++;

            if(textLine[index]!="."){
                throw new Exception ("se espera . despues de "+textLine[index-1]);

            }else{
                index++;
            }
            if(classTemp.methods.Any(obj=>obj.methodName==textLine[index])){
                action=textLine[index];
                index++;
                Debug.Log(textLine[index+1]+" error");
                if(textLine[index]=="("&& textLine[index+1]==")"){
                    index+=2;

                }else{
                    
                    throw new Exception ($" se esperaba () luego de llamar al metodo {textLine[index-1]}");
                }

                if(textLine[index]!=";"){
                    throw new Exception ("se esperaba ;");

                }
                return(action,classTemp);

            }else{

                throw new Exception($"el metodo { textLine[index]} no existe");
            }

        }else{

            throw new Exception ($"la variable {textLine[index]} no existe");
        }
        


    }
}
