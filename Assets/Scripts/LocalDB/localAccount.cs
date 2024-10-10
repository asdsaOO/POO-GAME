using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class localAccount : MonoBehaviour
{

    public static void createUserCredential(User user){
        if(!Directory.Exists(Application.dataPath+"/datasaves")){
            Directory.CreateDirectory(Application.dataPath+"/datasaves");
            
        }
        File.WriteAllText(PathFilesData.credentialPath,JsonConvert.SerializeObject(user));
        Debug.Log("se guardo correctamente");

    }

    //extraer datos de credenciales
    public static User getLocalCredentialData(){
        var localCredential = File.ReadAllText(PathFilesData.credentialPath);
        return JsonConvert.DeserializeObject<User>(localCredential);
    }


     public static void deleteLocalUserData(){
        File.Delete(PathFilesData.credentialPath);
    }
}
