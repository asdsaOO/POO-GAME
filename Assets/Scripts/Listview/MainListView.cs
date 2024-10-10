using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainListView : MonoBehaviour
{
    public TMP_InputField codeTxt;
    // Start is called before the first frame update
    //public string objectType;
    //public List<OClassIntance> list;
    //private int itemsNum;
    public GameObject  item;
    

    private List <GameObject> instancedItemObjects;
    private List <InstanceCreatedObjects> copiedList;

     private void Start() {
        
           
        instancedItemObjects= new List<GameObject>();
        copiedList= new List<InstanceCreatedObjects>(PreparationMng.mainClassInstancedList);
               

        
        
        //itemsNum=PreparationMng.classList.Count;
        


       fillList();
    }

    private void Update() {
        //depende que lista van a usar
       
            if(copiedList.Count!=PreparationMng.mainClassInstancedList.Count){
            copiedList=new List<InstanceCreatedObjects>(PreparationMng.mainClassInstancedList);
            updateList();
            //itemsNum=PreparationMng.classList.Count;
             
            
            
        }


        

        
    }

    private void fillList(){

        for(int i=0;i<copiedList.Count;i++){
            //extraemos el script y cambiamos el nombnre del item
             item.GetComponent<ItemScript>().itemNameTxt.text=copiedList[i].varName;
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
