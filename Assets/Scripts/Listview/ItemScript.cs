using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class ItemScript : MonoBehaviour
{
    
    
    
    public TMP_Text itemNameTxt;
    
   
    public void delete_item(){

        foreach(var item in PreparationMng.clasesCreatedList){

            if(item.classVar.className==itemNameTxt.text){
                PreparationMng.clasesCreatedList.Remove(item);
                return;
            }
        }

    }
    public void deleteInterfaceItem(){
         foreach(var item in PreparationMng.interfacesCreatedList){

            if(item.classVar.className==itemNameTxt.text){
                PreparationMng.interfacesCreatedList.Remove(item);
                return;
            }
        }

    }

    public void deleteMainInstanced (){
        
        foreach(var item in PreparationMng.mainClassInstancedList){
            

            if(item.varName==itemNameTxt.text){
                Debug.Log("es igual");
                PreparationMng.mainClassInstancedList.Remove(item);
                return;
            }else{

                Debug.Log($"{item.varName} no es igual que {itemNameTxt}");
            }
        }

    }
     public void infoItemClass() {
        string[] code = (from obj in PreparationMng.clasesCreatedList where obj.classVar.className==itemNameTxt.text select obj.code).First();
        TMP_InputField input = transform.parent.GetComponent<ListViewMng>().codeTxt;
        input.text="";

        foreach(var a in code){
            input.text+=a+"\n";
            

        }
        
    }

     public void infoItemInterface() {
        string[] code = (from obj in PreparationMng.interfacesCreatedList where obj.classVar.className==itemNameTxt.text select obj.code).First();
        TMP_InputField input = transform.parent.GetComponent<ListViewMng>().codeTxt;
        input.text="";

        foreach(var a in code){
            input.text+=a+"\n";
            

        }
        
    }
    public void infoMainItemInterface() {
        string[] code = (from obj in PreparationMng.mainClassInstancedList where obj.varName==itemNameTxt.text select obj.code).First();
        foreach(var a in PreparationMng.mainClassInstancedList){
            Debug.Log(a.varName);

        }
        TMP_InputField input = transform.parent.GetComponent<MainListView>().codeTxt;
        input.text="";

        foreach(var a in code){
            input.text+=a+"\n";
            

        }
        
    }


}
