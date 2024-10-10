using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CreatePanelClass : MonoBehaviour
{
    CreatePanelClass instance_;

    public GameObject tutoPanel;
    public TMP_InputField code;
    public TMP_InputField debugText;

    GameRules rules = new GameRules();
    //lista de errores en la escitura de codigo

    //lista de imtems instanciados
    
    



    //
    public static string error;

    // Start is called before the first frame update

    private void Awake() {
        if(instance_!=null &&instance_!=this){
            Destroy(this);


        }else {
            instance_=this;


        }
    }
    void Start()
    {
        error="";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBtn(){
        try{
        
        //code.lineType=TMP_InputField.LineType.SingleLine;
        
        PreparationMng.clasesCreatedList.Add( ConsolesText.analiceClassConsole(code.text.Replace('\n',' ')));
        //mapea los datos
        debugText.text="La clase se creo correctamente";
        Debug.Log($"la a primera variable de la clase {PreparationMng.clasesCreatedList[0].classVar.className} es {PreparationMng.clasesCreatedList[0].classVar.atributes[0].varAccess}");


        }catch (Exception e)
        {
            Debug.Log("el error que vemos es:"+e);
            debugText.text=e.Message;
        }
    
       
        
    }
    public void createInterfaceBtn(){
        try{

            PreparationMng.interfacesCreatedList.Add(ConsolesText.analyceInterfaceConsole(code.text.Replace('\n',' ')));
            debugText.text="el interfaz se creo correctamente";

        }catch(Exception e){
            Debug.Log("el error que vemos es:"+e);
            debugText.text=e.Message;
            
        }
    }

    public void createMainObject(){
        try{
            InstanceCreatedObjects toAdd=ConsolesText.analyceMain(code.text.Replace('\n',' '));
            rules.verifyRules(PreparationMng.mainClassInstancedList,toAdd);


            PreparationMng.mainClassInstancedList.Add(toAdd);

            foreach(var a in PreparationMng.mainClassInstancedList){
                Debug.Log($"el tipo de clase es {a.classVar.className}");
                foreach(var b in a.classVar.atributes){
                    Debug.Log(($"la variable es {b.varName} y su valor {b.varValue}"));

                }

            }

            //PreparationMng.mainClassInstancedList.Add(ConsolesText.analyceInterfaceConsole(code.text.Replace('\n',' ')));
            debugText.text="el objeto se instancio";

        }catch(Exception e){
            Debug.Log("el error que vemos es:"+e);
            debugText.text=e.Message;
            
        }


    }
    public void openTuto(){
        Instantiate(tutoPanel,transform);
    }

    

   

   
}
