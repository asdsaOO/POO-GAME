using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

using UnityEngine;

public class localDataSave : MonoBehaviour
{
    // Start is called before the first frame update
    public static async  Task<bool> loadProgress(){
        User user =JsonConvert.DeserializeObject<User>(File.ReadAllText(PathFilesData.credentialPath));
        var progresData = await SavesProgressServices.getProgress(user);
        Debug.Log(progresData);
        File.WriteAllText(PathFilesData.saveprogressPath,progresData);
        return true;

    }

    public static  void deleteProgress(){
         File.Delete(PathFilesData.saveprogressPath);

    }

    public   static Progress GetLocalProgress(){

        var progress= File.ReadAllText(PathFilesData.saveprogressPath);
        return JsonConvert.DeserializeObject<Progress>(progress);

    }

    public static void overwrite (Progress newProgress){

        Debug.Log("nuevo nivel en overwrite es"+ newProgress.lvl);
        var progress = JsonConvert.SerializeObject(newProgress);
        Debug.Log("progreso a guardar "+ progress);
        File.WriteAllText(PathFilesData.saveprogressPath,progress);
    

    }

    
}
