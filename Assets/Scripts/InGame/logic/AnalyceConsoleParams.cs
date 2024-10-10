using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;
using Org.BouncyCastle.Tls;
using System.Numerics;
using UnityEngine.UIElements;


public class AnalyceConsoleParams 
{
    
    //analizar parametros de una clase
    public static OClassIntance classParams (OClassIntance classInstanced, bool functionActive, string[] textVector,int lvl){
        Debug.Log("buscando parametros");
       
        //estas variables serviran para determinar si se creo todas las variables de la clase y sus metodos
        bool variablesAssign=false, ConstructorAssgn=false, methodsAssign=false;
        

        for(int i=1;i<textVector.Length-1;i++){
            int index=0;
            if(variablesAssign==false){
                string access="", varType="";
                
                //separar el texto en lineas
                string[]textVectorLine = textVector[i].Split(" ",StringSplitOptions.RemoveEmptyEntries);
                access=findAccess(textVectorLine);

                
                Debug.Log($"el acceso de la variable es {access}");
                //recorrer el index de forma manual utilizando una condicional para determinar si el acceso ocupa 2 campos o solo 1
                if(paramsInGame.accesInstance.Contains( textVectorLine[index])){
                    if(textVectorLine[index+1]=="static"){
                        index+=2;

                    }else{
                        index+=1;
                    }
                    
                }else if(textVectorLine[index]=="static"){
                    index++;
                }

                Debug.Log("el indice es "+ index+" y acceso es "+access);

                ////////////////////////////////////////////////
                ///ERROR: NO CONTROLA INDEX SUPERIOR AL TAMAÑO DEL ARRAY
                if(varTypeExist(textVectorLine[index])){

                    varType= textVectorLine[index];
                    index++;
                    Debug.Log("el tipo de variable es "+varType);
                }else{
                    Debug.Log($"ERROR: No existe ese tipo de variable {textVectorLine[index]}");

                    throw new Exception($"ERROR: No existe ese tipo de variable {textVectorLine[index]}");
                }

                verifyVariable(index,classInstanced,textVectorLine[index],textVectorLine[index-1]);
                index++;

                if(textVectorLine[index]!=";"){
                    throw new Exception ("ERROR: Se espera ;");

                }

            ////////////////////////////////
            ///VERIFICAR SI TODOS SUS ATRIBUTOS SE CREARON
            ///
            variablesAssign=isCreatedAtributes(classInstanced);
              //verificar la variable

            }else if(!ConstructorAssgn&&lvl>=3){
               Debug.Log("analizara cosntructor");

               var constructorData=verifyConstructor(i,textVector,classInstanced); 
               classInstanced.constructor=constructorData.Item1;
               i=constructorData.Item2;
               //i++;
               
               foreach(var a in classInstanced.constructor){
                Debug.Log(a);
               }
               ConstructorAssgn=true;
               Debug.Log("linea sig al constructor"+textVector[i]);


            }else if(!methodsAssign&&lvl>=2){
                Debug.Log("analizando methodos linea: "+textVector[i] );
                var methodVeridyData= verifyMethods(i,classInstanced,textVector);
                classInstanced=methodVeridyData.Item2;
                i=methodVeridyData.Item1;
                methodsAssign=true;


            }
        }
        if(!verifyClassVariablesInstanced(classInstanced)){
            throw new Exception("ERROR: Debes crear todas las variables de la clase");
        }else{
            Debug.Log("la clase se creo correctamente... por ahora");
            //Debug.Log($"ERRACCESS: variable la clase creada, si primer acceso es {classInstanced.atributes[0].varAccess}");
            return classInstanced;
        }

    }

    private static (int,OClassIntance) verifyMethods (int index, OClassIntance instanced, string[] vectorText){
        while(index<vectorText.Length-1){
            string access, methodName;
            string [] vectorLine=vectorText[index].Split(' ');
            int indexLine=0;

            access=findAccess(vectorLine);
            //////
            ///
            /// 
            /// 
            /// 
            if(paramsInGame.accesInstance.Contains( vectorLine[indexLine])){
                    if(vectorLine[indexLine+1]=="static"){
                        indexLine+=2;

                    }else{
                        indexLine+=1;
                    }
                    
                }else if(vectorLine[indexLine]=="static"){
                    indexLine++;
                }
            ///COMPLETAR CON EL RESTO
            ///
            //Debug.Log("acceso del constructor "+access);

            if(vectorLine[indexLine]!="void"){
                Debug.Log("ERROR EN "+vectorLine[indexLine]);
                throw new Exception("ERROR: Solo se admiten funciones vacias para crear");

            }else{
                
                indexLine++;
                
            }
            if(instanced.methods.Any(obj=>obj.methodName==vectorLine[indexLine])){
                methodName=vectorLine[indexLine];
                indexLine++;

            }else{
                throw new Exception($"ERROR: el nombre {vectorLine[indexLine] } no existen para metodos ");
            }
            if(vectorLine[indexLine]=="("&& vectorLine[indexLine+1]==")"){
                indexLine+=2;

            }else{
                throw new Exception ($"ERROR: se esperaba () luego de nombrar a la funcion");
            }
            if(vectorLine[indexLine]=="{"){
                index++;

            }else{
                throw new Exception("ERROR METODO: se espera { luego de crear el metodo "+methodName);
            }

            if(instanced.methods.Any(obj=>obj.methodAction==vectorText[index])){
                activeMethod(instanced,methodName);
                index++;

            }else{
                throw new Exception ($"ERROR METODO: la accion {vectorText[index]} no esta relacionado con este metodo ");
            }
            if(vectorText[index]=="}"&&vectorText.Length>index){
                index++;

            }else {

                throw new Exception( $"ERROR METODO: el metodo {methodName} no esta siendo cerrado");
            }




        }

        return (index,instanced);

    }


    private static (string[],int) verifyConstructor(int index, string[] textVector, OClassIntance instanced){

        /////////////////////////////////////////////////////////////////////////////////////////
        ///ESTA FUNCION ES PARA IDENTIFICAR EL CONSTRUCTOR, DEVOLVERA EL INDICE DEL RECORRUDO////
        //////////////////////////////////////////////////////////////////////////////////////////
        string [,] atributesVector =new string [instanced.atributes.Count,2];
        //primero gaurdamos los valores en una matriz  2x2 donde el primer valor sera la varaible y el segundo si fue utilizado en el cosntructor

        for(int i=0;i<instanced.atributes.Count;i++){
            atributesVector[i,0]="_"+instanced.atributes[i].varName;
            atributesVector[i,1]="no assign";

        }

        string [] textVectorLine=textVector[index].Split(' ',StringSplitOptions.RemoveEmptyEntries);
        int indexLine=0;

        string access =findAccess(textVectorLine);

        /////////////////////////////////////////////////////////
        ///ESPACIO QUE OCUPA EL ACCESO CREAR FUNCION

        if(paramsInGame.accesInstance.Contains( textVectorLine[indexLine])){
            if(textVectorLine[indexLine+1]=="static"){
                indexLine+=2;

            }else{
                indexLine+=1;
            }
                    
        }else if(textVectorLine[indexLine]=="static"){
            indexLine++;
        }
        /////////////////////////////////////////////////////////////////

        if(textVectorLine[indexLine]==instanced.className){
            indexLine++;

        }else{
            Debug.Log($"ERROR: EL constructor debe llevar el mismo nombre de la clase {textVectorLine[indexLine]}");

            throw new Exception("ERROR: EL constructor debe llevar el mismo nombre de la clase");
        }

        if(textVectorLine[indexLine]=="("){
            indexLine++;
        }else {

            throw new Exception ("ERROR CONSTRUCTOR: Falta parentesis");
        }

        while(textVectorLine[indexLine]!=")"){
            if(varTypeExist(textVectorLine[indexLine])){
                indexLine++;

            }else{
                Debug.Log("");

                throw new Exception ($"ERROR CONSTRUCTOR: El tipo de variable {textVectorLine[indexLine]} no existe");
            }

            atributesVector = paramExistConstructor(atributesVector, textVectorLine[indexLine]);

            indexLine++;

            if(textVectorLine[indexLine]!=","&& textVectorLine[indexLine]!= ")"){
                throw new Exception ("ERROR CONSTRUCTOR: Se esperaba ,");

            }else if(textVectorLine[indexLine]!= ")" ){
                indexLine ++;


            }
            

        }
        indexLine++;
        if(textVectorLine[indexLine]!="{"){
            throw  new Exception("ERROR CONSTRUCTOR: Se espera { despues de crear los parametros del constructor");

        }
        Debug.Log("fin de analizar parametros de constructor");

        index++;
/////////////////////////////////////////////////////////////////////////////
///ASIGNAR VARIABLES DEL CONSTRUCTOR;
        while(textVector[index]!="}"){
            string atributeAssigned;
            string varAssingned;
            
            indexLine=0;
            textVectorLine=textVector[index].Split(' ',StringSplitOptions.RemoveEmptyEntries);
            if(instanced.atributes.Any(obj=>obj.varName==textVectorLine[indexLine])){
                atributeAssigned=textVectorLine[indexLine];
                indexLine++;
                
            }else{
                throw new Exception($"ERRO CONSTRUCTOR: El atributo {textVectorLine[indexLine]} no existe");
            }
            if(textVectorLine[indexLine]!="="){
                throw new Exception("ERROR CONSTRUCTOR: Se esperaba = despues de la variable "+atributeAssigned);
            }else{
                indexLine++;
            }
            if(textVectorLine[indexLine]=="_"+atributeAssigned){
                varAssingned=textVectorLine[indexLine];
                indexLine++;

            }else{
                throw new Exception($"ERROR CONSTRUCTOR: Sebes igualar la variable {atributeAssigned} a su referente en el constructor");
            }

            if(textVectorLine[indexLine]!=";"){
                throw new Exception($"ERROR CONSTRUCTOR: Se esperaba ; luego de {varAssingned}");

            }
            assignParamsConstructor(atributesVector,varAssingned);
            index++;        
           ////////////////////////////////////////////////////////
           ///ACA QUEDA
           ///////////////////////////////////////////////////////
        }
        return (convertToVectorParams(atributesVector),index);
        //verificar que el constructor sea 
    }
    private static bool isCreatedAtributes(OClassIntance instanced){
        foreach(var a in instanced.atributes){
            if (a.varValue==null){

                return false;
            }

        }

        return true;



    }

    public static void verifyVariable(int index, OClassIntance classInstanced, string varname, string varType){
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///VERIFICAR SI LA VARIABLE QUE SE ESTA CREANDO EXISTE EN LA CLASE Y NO ESTA INSTANCIADA ANTES//
        ////////////////////////////////////////////////////////////////////////////////////////////////
        if(classInstanced.atributes.Any(obj=>obj.varName==varname)){
            Debug.Log("la variable es "+varname);
            for(int j=0;j<classInstanced.atributes.Count;j++){
                if( classInstanced.atributes[j].varName==varname ){

                    if(classInstanced.atributes[j].varType==varType&&classInstanced.atributes[j].varValue==null){
                        classInstanced.atributes[j].varValue="";     
                                //final, aun falta hacer cosas
                    }else{

                        Debug.Log( classInstanced.atributes[j].varValue+" el valor de la variable");

                        throw new Exception ($"ERROR: La variable {varname} no coincide con su tipo de variable el tipo de variable es {classInstanced.atributes[j].varType} el valor es {classInstanced.atributes[j].varValue}" );
                                 
                    }
                            
                }

            }

        }else{
             throw new Exception($"ERROR: la variable  {varname} no existe para esta clase");
        }

    }


    public static bool verifyClassVariablesInstanced(OClassIntance classIstanced){
        //bool a=false;

        /////////////////verificar si todas los atributos de la clase fueron iniciados
        ///
        foreach(var item in classIstanced.atributes){
            if(item.varValue==null){
                return false;
            }
        }
        return true;
    }
    public static bool varTypeExist( string varType){
        if(paramsInGame.typeVarInstance.Contains(varType)){
            return true;

        }else{

            return false;
        }

    }

    public static string findAccess(string[]vectorLine){

        string access="none";
        if(accessExist(vectorLine[0])){
            access=vectorLine[0];
            if(vectorLine.Length>2&& vectorLine[1]=="static"){
                access=access+" "+"static";
            }

        }else{ 
            access="private";    
            if(vectorLine[0]=="static"){
                access="private static";

            }
        }
        return access;


    }
    public static bool accessExist(string access){
        if(paramsInGame.accesInstance.Contains(access)){
            return true;
        }else{

            return false;
        }


    }

    private static string[]convertToVectorParams(string[,]  arrayParams){
        string [] vectorPArams= new string [arrayParams.GetLength(0)];


        for(int i=0;i<arrayParams.GetLength(0);i++){
            vectorPArams[i]=arrayParams[i,0];


        }
        return vectorPArams;

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    private static string[,] paramExistConstructor (string [,] vectorParams, string variable){
        for (int i=0; i<vectorParams.GetLength(0);i++){
            if(vectorParams[i,0]==variable && vectorParams[i,1]=="no assign"){
                vectorParams[i,1]="created";

                return sortVariables(vectorParams,variable);
                

            }

        }
        Debug.Log($"ERROR: el dato de error es {variable}");

        throw new Exception($"ERROR: Los parametros del constructor deben tener un nombre similar que las variables de la clase por ejemplo '_nombreVariable', quiza estas repitendo {variable} ");
        
    }
    private static string[,] sortVariables (string [,] vectorParams, string param){

        for(int i=0; i<vectorParams.GetLength(0)-1;i++){
            string[] aux ={vectorParams[i,0],vectorParams[i,1] };
            if(vectorParams[i,0]==param){
                //aux[1]="created";
                vectorParams[i,0]= vectorParams[i+1,0];
                vectorParams[i,1]= vectorParams[i+1,1];

                vectorParams[i+1,0]= aux[0];
                vectorParams[i+1,1]= aux[1];
                Debug.Log("datos del vector " +aux[0] +aux[1]);
                

            }

        }

        return vectorParams;

    }

    private static string[,] assignParamsConstructor (string[,] vectorParams , string param){
        for(int i=0;i<vectorParams.GetLength(0);i++){
           if(vectorParams[i,0]==param){
            vectorParams[i,1]="assigned";
            return vectorParams;

           }


        }
        Debug.Log("Nombre de la variable "+param);
        throw new Exception($"ERROR CONSTRUCTOR: La variable {param} no se puede reconocer");

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public static OClassIntance activeMethod(OClassIntance instanced, string methodName){

        foreach(var a in instanced.methods){

            if(a.methodName==methodName){
                a.methodActive=true;
                return instanced;
            }
        }
        throw new Exception ($"ERROR METODOS: el metodo {methodName} no esta familiarizado en esta clase");
    }

    
}
