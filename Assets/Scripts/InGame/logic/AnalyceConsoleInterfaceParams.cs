using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  
using System;

public class AnalyceConsoleInterfaceParams 
{
    // Start is called before the first frame update

    public static OClassIntance verifyMethods (int index, OClassIntance instanced, string[] vectorText){
        while(instanced.methods.Any(obj=>obj.methodActive==false)){
            string access, methodName;
            string [] vectorLine=vectorText[index].Split(' ');
            int indexLine=0;

            access=AnalyceConsoleParams.findAccess(vectorLine);
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
            Debug.Log("acceso del constructor "+access);

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
            if(vectorLine[indexLine]==";"){
                AnalyceConsoleParams.activeMethod(instanced,methodName);
                indexLine+=2;

            }else{
                throw new Exception ($"ERROR: se esperaba ; luego de nombrar a la funcion");
            }

            
        }
        return instanced;
   }
   /*private OClassIntance findMethodsInterfaces(OClassIntance instanced, int index, string[] vectorText) {
    while (index < vectorText.Length -1){
        int indexLine=0;
        string[] vectorLine= vectorText[index].Split(' ');
        if(vectorLine[0]!="public"){
            throw new Exception("ERROR INTERFAZ: solos e pueden crear metodos public en la interfaz");

        }else{
            indexLine++;
        }

        


    }
    return instanced;

   }*/
}
