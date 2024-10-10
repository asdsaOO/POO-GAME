using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceCreatedObjects : CreatedObjects{

    public string varName;

    public InstanceCreatedObjects(OClassIntance _classVar, string[] _code, string _varName):base(_classVar,_code){
        varName=_varName;
    }
}
