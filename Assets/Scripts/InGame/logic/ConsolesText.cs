using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using System;
using Org.BouncyCastle.Asn1.Crmf;

public class ConsolesText : MonoBehaviour
{


    public static CreatedObjects analiceClassConsole(string text){
         Debug.Log("funcion analizar");
        string [] mapText=mapFormatText(text);
        string []textLineVector=mapText[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        string access="", className="";
        //primero saber si esta nombrando una clase existente
        int index=0;
        
        if(paramsInGame.accesInstance.Contains(textLineVector[index])){
                
            if(textLineVector[1]=="static"){
                
                access=textLineVector[0]+" "+textLineVector[1];
                index+=2;

            }else{
                access=textLineVector[0];
                index++;
            }
            //Debug.Log("acceso: "+access);

            if(textLineVector[index]=="class"){
                index++;
                 // verificar que la clase exista con linq
                 if(paramsInGame.gameClassessActive.Any(obj=>obj.className==textLineVector[index])){
                    Debug.Log(textLineVector[index]+ " existe esta clase");
                    if(textLineVector[textLineVector.Length-1]=="{"&&mapText[mapText.Length-1]=="}"){
                        className=textLineVector[index];
                        Debug.Log("formato valido");
                    }else{
                        throw new Exception($"ERROR: Problema en reconocer llaves {textLineVector[textLineVector.Length-1]} y { mapText[mapText.Length-1]}");
                    }
                }else{

                    Debug.Log(textLineVector[index]+ " NO existe esta clase");
                    //CreatePanelClass.error= "la clase que intentas crear no existe en el juego";
                    throw new Exception("la clase que intentas crear no existe en el juego");

                }
            }else{
                Debug.Log("debes crear el tipo class");
                CreatePanelClass.error= "ERROR: Solo puedes crear clases en este panel";
                throw new Exception("la clase que intentas crear no existe en el juego");
            }
        }else{
            //Debug.Log("problemas de sintaxis: no existe ese acceso");
            //CreatePanelClass.error= "ERROR: Sintaxis de acceso no identificado";
            throw new Exception("Eror de syntaxis");

        }

        if(className==""){
            //CreatePanelClass.error="ERROR: Problemas de sintaxis";
            throw new ArgumentException("problemas en la sintaxis del codigo no se reconoce el nombre de la clase");
             


        }else{
            
            //en caso de reconocer la clase tenemos que instanciar el prototipo de la clase creada asignando valores establecidos anteriormente
            OClassIntance tempClass = (from obj in paramsInGame.gameClassessActive where obj.className==className select obj).First();

            OClassIntance createdClass = tempClass.DeepCopy();

            /////////////////////////////////////////////////////////////////////////////////////

            var inheritData=analyceInheritance(textLineVector, index+1,createdClass);
            index=inheritData.Item2;
            createdClass=inheritData.Item1;
            ////////////////////////////////////////////////////////////////////////////////////////


            createdClass.className=className;
            createdClass.classAccess=access;

            //ahora se debe validad sus atributos y metodos
            Debug.Log("la clase que creaste es: "+createdClass.className+" con un acceso de "+createdClass.classAccess); 
            ///////////////////
            ///PONER ACA HERENCIAS E INTERFACES
            //////////////////
            ///
            CreatedObjects objectCreated= new CreatedObjects(AnalyceConsoleParams.classParams(createdClass,false,mapText,paramsInGame.LvlIngame),mapText); 
            verifyMethodsRequired(objectCreated.classVar);

            return objectCreated;
        }
        //Debug.Log("acabo la funcion");
    }

    /////////////////////////////////////////////////////////////////////////////////
    ///Interfaces
    public static CreatedObjects analyceInterfaceConsole(string text){
         Debug.Log("funcion analizar interface code");
        string [] mapText=mapFormatText(text);
        string []textLineVector=mapText[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);

        string access="", interfaceName="";
        //primero saber si esta nombrando una clase existente
        int index=0;
        
        if(paramsInGame.accesInstance.Contains(textLineVector[index])){
                
            if(textLineVector[1]=="static"){
                
                access=textLineVector[0]+" "+textLineVector[1];
                index+=2;

            }else{
                access=textLineVector[0];
                index++;
            }
            //Debug.Log("acceso: "+access);

            if(textLineVector[index]=="interface"){
                index++;
                 // verificar que la clase exista con linq
                 if(textLineVector[index][0]=='I'&&paramsInGame.gameInterfacesActive.Any(obj=>obj.className==textLineVector[index])){
                    Debug.Log(textLineVector[index]+ " existe esta interface");
                    if(textLineVector[textLineVector.Length-1]=="{"&&mapText[mapText.Length-1]=="}"){
                        interfaceName=textLineVector[index];
                        Debug.Log("formato valido");
                    }else{
                        throw new Exception($"ERROR: Problema en reconocer llaves {textLineVector[textLineVector.Length-1]} y { mapText[mapText.Length-1]}");
                    }
                }else{

                    Debug.Log(textLineVector[index]+ " NO existe esta interface");
                    //CreatePanelClass.error= "la clase que intentas crear no existe en el juego";
                    throw new Exception("la clase que intentas crear no existe en el juego");

                }
            }else{
                Debug.Log("debes crear el tipo interface");
                CreatePanelClass.error= "ERROR: Solo puedes crear interfaces en este panel";
                throw new Exception("la interface que intentas crear no existe en el juego");
            }
        }else{
            //Debug.Log("problemas de sintaxis: no existe ese acceso");
            //CreatePanelClass.error= "ERROR: Sintaxis de acceso no identificado";
            throw new Exception("Eror de syntaxis");

        }

        if(interfaceName==""){
            //CreatePanelClass.error="ERROR: Problemas de sintaxis";
            throw new ArgumentException("problemas en la sintaxis del codigo no se reconoce el nombre de la interface");
             


        }else{
            
            //en caso de reconocer la clase tenemos que instanciar el prototipo de la clase creada asignando valores establecidos anteriormente
            OClassIntance tempClass = (from obj in paramsInGame.gameInterfacesActive where obj.className==interfaceName select obj).First();

            OClassIntance createdInterface = tempClass.DeepCopy();
            createdInterface.className=interfaceName;
            createdInterface.classAccess=access;

            //ahora se debe validad sus atributos y metodos
            Debug.Log("la clase que creaste es: "+createdInterface.className+" con un acceso de "+createdInterface.classAccess); 
            
            CreatedObjects objectCreated= new CreatedObjects(AnalyceConsoleInterfaceParams.verifyMethods(1,createdInterface,mapText),mapText);



            return objectCreated;
        }
        

    }
    public static InstanceCreatedObjects analyceMain(string text){
        string [] mapText=mapFormatText(text);
        return AmalyceMainConsole.analyceInstance(mapText);

    }
    


    

    public  static string [] mapFormatText(string text){

        string textFormat="";


        for(int i =0;i<text.Length;i++){
            bool space=false;
            //the format needs a void space for each simbol
            if(text[i]=='='||text[i]=='('||text[i]==')'||text[i]=='{'||text[i]==';'||text[i]==',' || text[i]=='.' || text[i]==':'){//////////////////////////////////////
                
                if(i>0 && text[i-1]!=' '){
                    textFormat=textFormat+" "; 
                }
                if(i<text.Length-1&& text[i+1]!=' '){
                    space=true; 
                }
            }

            //ASSIGN TEXT TO TEXTFORMAT
            textFormat=textFormat+text[i];
            if(space){
                textFormat=textFormat+" ";
            }
            //separate final line code in a vector using split and the character "-" and add space when its necessary

            if(text[i]==';'||text[i]=='{' || text[i]=='}'){
                textFormat=textFormat +"-";   
            }
            
        }
        //use split to convert vector format
       string []vector=textFormat.Split(new [] {'-','\n'},StringSplitOptions.RemoveEmptyEntries). Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();//////////////////////////////////////////////////
         //use Trim to delete void spaces
         Debug.Log("el tamaño del vector " +vector.Length.ToString());
         /*foreach(string item in vector){

            Debug.Log(item);
         }*/

        for(int i=0;i<vector.Length;i++){
            //Debug.Log(i);

            if((vector[i])[0]==' ' || (vector[i])[vector[i].Length-1]== ' '){
                vector[i]=vector[i].Trim();

            }   
            //crear otro vector
            string newCharancer="";
            string[] vectorAux =vector[i].Split(" ",StringSplitOptions.RemoveEmptyEntries);

            foreach(string item in vectorAux ){
                if(item!=""){
                    if(newCharancer==""){
                        newCharancer=item;
                    }else{
                        newCharancer=newCharancer+" "+item;

                    }
                    
                }
            }
            vector[i]=newCharancer;
            
            Debug.Log(vector[i]);
            Debug.Log( vector[i].Length);
        }


        return vector;

    }
    private static (OClassIntance,int) analyceInheritance(string[]vectorLine, int indexline, OClassIntance instanced){
        Debug.Log("herencia el indice es "+indexline+" valor "+vectorLine[indexline]);
        bool clases=false;
        bool interfaces=false;  
        if(vectorLine[indexline]==":"){
            Debug.Log("Analizando herencia....");
            indexline++;
            if(PreparationMng.interfacesCreatedList.Any(obj=>obj.classVar.className==vectorLine[indexline])){
                ///////////////////////////////////////////
                ///ES INTERFACE
                CreatedObjects interfaceCopy=(from obj in PreparationMng.interfacesCreatedList where obj.classVar.className==vectorLine[indexline] select obj).First();
                instanced=assignInherit(interfaceCopy.classVar,instanced,"interface");
                indexline++;
                interfaces=true;
                return (instanced,indexline);
            }else {

                throw new Exception ("No se reconoce la herencia");
            }


        }else{
            return( instanced,indexline);
        }

    }

    private static OClassIntance assignInherit(OClassIntance parent, OClassIntance childClass, string parentType){

        if(parentType=="interface"){
            foreach(var parentMethod in parent.methods){
                for(int i=0;i<childClass.methods.Count;i++){
                    if(parentMethod.methodName==childClass.methods[i].methodName){
                        childClass.methods[i].requided=true;

                    }

                }
                

            }
            return childClass;

        }else{
            throw new Exception ("ERROR: aun falta poner abstractos");
        }

        



    }
    private static void  verifyMethodsRequired(OClassIntance instanced){
        foreach(var a in instanced.methods){
            if(a.methodActive==false && a.requided){
                throw new Exception($"ERROR: el metodo {a.methodName} es requerido por herencia de alguna clase abstracta o interfaz probablemente");

            }

        }

    }
}
