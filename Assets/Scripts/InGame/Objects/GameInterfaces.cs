using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInterfaces 
{
    // Start is called before the first frame update
    public static OClassIntance Ireparable = new OClassIntance(
        "Ireparable",
        "editAccess",
        false,
        new List<OVarInstance>(),
        new List<OMethodsInstance>{
            new OMethodsInstance("reparar","editAcces",false,null,false),
        },
        null

     );

     public static OClassIntance Isustituible = new OClassIntance(
        "Isustituible",
        "editAccess",
        false,
        new List<OVarInstance>(),
        new List<OMethodsInstance>{
            new OMethodsInstance("sustituir","editAcces",false,null,false),
        },
        null

     );
}
