using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedObjects : MonoBehaviour
{
    // Start is called before the first frame update
   public OClassIntance classVar;
   public string[] code;

   public CreatedObjects(OClassIntance _classVar, string[] _code){

    classVar=_classVar;
    code=_code;
   }

   public CreatedObjects copy(){

    return new CreatedObjects(classVar.DeepCopy(),code);

   }

}
