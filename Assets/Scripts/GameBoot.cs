using System.Collections;
using System.Collections.Generic;
using System.IO;
using Org.BouncyCastle.Asn1.Misc;
using Unity.Mathematics;
using UnityEngine;
using Newtonsoft.Json;


public class GameBoot : MonoBehaviour
{
    string datapath;

    private void Awake() {  
        datapath= Application.dataPath+"/datasaves/UserCredential.json";
        trySingIn();

       
        //trySingIn();
    }

//iniciar session de forma inmediata, en caso de que exista el archivo de crendencial de usuario
    private async void trySingIn (){
        if(File.Exists(PathFilesData.credentialPath)){
            //convertir datos locales de los files en objecto
            string localCredential = File.ReadAllText(PathFilesData.credentialPath);
            var userLocal= JsonConvert.DeserializeObject<User>(localCredential);
            Debug.Log(userLocal.email+" "+userLocal.password+" "+userLocal.userName);
            

            //realizar el servicio y verificar su completo el inicio de sesion
            StatusResponseData resp= await SignInService.authInService(userLocal.email,userLocal.password);
            //var respObject= JsonConvert.DeserializeObject<StatusResponseData>(resp);
            if(resp.description=="succesfull"){
                ScenesMng.changeScene(1);


            }

        }else{
            Debug.Log("no existe cuenta");
        }

    }
    //cuando lo llamas directamente no instancia
    /*public static void CreateDataSaves(string datajson){
        string path=Application.dataPath+"/datasaves/UserCredential.json";

        if(!Directory.Exists(path)){
            Directory.CreateDirectory(Application.dataPath+"/datasaves");
            File.WriteAllText(path,datajson);
            Debug.Log("se guardo correctamente");

        }else{
            File.WriteAllText(path,datajson);
            Debug.Log("se guardo correctamente");

        }
       


    }*/



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
