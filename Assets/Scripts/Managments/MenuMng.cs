using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Operators;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMng : MonoBehaviour
{
    public TextMeshProUGUI score;
    MenuMng instance;
    public static bool [] lvlsActive ={false,false,false,false,false,false,false,false};
    public TextMeshProUGUI userNamePanel;
    public static User userData;
    public static Progress progress;
    // Start is called before the first frame update
    private void Awake() {
        
        if(instance!=null&& instance!=this){
            Destroy(this.gameObject);
            Destroy(this);
            Debug.Log("ya existe");
        }else{

            Debug.Log("nuevo menu");
            instance=this;
            

        }
        loadDataScene();
        
        
    }
    //cargar datos del usuario, credencial y demas;
    private void loadDataScene(){
        userData=localAccount.getLocalCredentialData();
        userNamePanel.SetText(userData.userName);
        progress=localDataSave.GetLocalProgress();
        score.text="Score Total: "+progress.score.ToString();
        Debug.Log("nivel del jugador"+progress.lvl.ToString());
        iniciateLvls();
    }

    public void sigOutBtn(){
        localAccount.deleteLocalUserData();
        localDataSave.deleteProgress();
        ScenesMng.changeScene(0);
    }
    
    private void iniciateLvls(){
        for(int i=0;i<lvlsActive.Length;i++){
            if(progress.lvl>i){
                 lvlsActive[i]=true;

            }else{
                lvlsActive[i]=false;
            }
           
        }
    }

    public void saveCloud(){
        SavesProgressServices.updateCloudProgress(localAccount.getLocalCredentialData(),localDataSave.GetLocalProgress());
    }
    private void Start() {
        
    }

   
   


}
