using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class PathFilesData : MonoBehaviour
{
    // Start is called before the first frame update
   public static string credentialPath=Application.dataPath+"/datasaves/UserCredential.json";

   public static string saveprogressPath=Application.dataPath+"/datasaves/localProgress.json";

   public static string saveLayerInp=Application.dataPath+"/datasaves/layerInp.txt";
   public static string saveLayerOut=Application.dataPath+"/datasaves/layerOut.txt";
}
