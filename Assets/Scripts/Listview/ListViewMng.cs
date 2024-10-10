using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Michsky.MUIP;
using TMPro;
using UnityEngine;

public class ListViewMng : MonoBehaviour
{
    public TMP_InputField codeTxt;

    //un string par determinar que tipo de dato se va a mostrostrarr: como clase, interfaz y demas estructuras


    public string objectType;
    //public List<OClassIntance> list;
    //private int itemsNum;
    public GameObject  item;
    

    private List <GameObject> instancedItemObjects;
    private List <CreatedObjects> copiedList;

    

   
    // Start is called before the first frame update
    private void Awake() {        

    }
    

    private void Start() {
        switch(objectType){
            case "class":
            instancedItemObjects= new List<GameObject>();
            copiedList= new List<CreatedObjects>(PreparationMng.clasesCreatedList);
                break;
            case "interface":
            instancedItemObjects= new List<GameObject>();
            copiedList= new List<CreatedObjects>(PreparationMng.interfacesCreatedList);
                break;

           /* case "main":
            instancedItemObjects= new List<GameObject>();
            copiedList= new List<CreatedObjects>(PreparationMng.mainCreatedList);
                break;*/

        }
        
        //itemsNum=PreparationMng.classList.Count;
        


       fillList();
    }

    private void Update() {
        //depende que lista van a usar
        switch (objectType){
            case "class":
            if(copiedList.Count!=PreparationMng.clasesCreatedList.Count){
            copiedList=new List<CreatedObjects>(PreparationMng.clasesCreatedList);
            updateList();
            //itemsNum=PreparationMng.classList.Count;
             
            }
            break;

            case "interface":
            if(copiedList.Count!=PreparationMng.interfacesCreatedList.Count){
                Debug.Log("cambio el numero");
            copiedList=new List<CreatedObjects>(PreparationMng.interfacesCreatedList);
            updateList();
            //itemsNum=PreparationMng.classList.Count;
             
            }
            break;

            /*case "main":
             if(copiedList.Count!=PreparationMng.mainCreatedList.Count){
                Debug.Log("cambio el numero");
            copiedList=new List<CreatedObjects>(PreparationMng.mainCreatedList);
            updateList();
            //itemsNum=PreparationMng.classList.Count;
             
            }

            break;*/
        }


        

        
    }

    private void fillList(){

        for(int i=0;i<copiedList.Count;i++){
            //extraemos el script y cambiamos el nombnre del item
             item.GetComponent<ItemScript>().itemNameTxt.text=copiedList[i].classVar.className;
             //creamos en la lista 
             var itemInstance = Instantiate (item,transform);
             instancedItemObjects.Add(itemInstance);



            

            
        }

    }
    private void updateList (){
        deleteList();

        fillList();

    }

    private void deleteList(){

        foreach(var a in instancedItemObjects){
            Destroy(a);
        }
    }




}
