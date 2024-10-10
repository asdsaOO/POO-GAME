using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMethodsInstance 
{
    // Start is called before the first frame update
   public  string methodName;
    public  string methodAccess;
    
    public bool requided;

    public string methodAction;

    public bool methodActive;
    

    public OMethodsInstance (string _methodName,string _methodAccess, bool _required, string _methodAction,bool _methodActive){
        this.methodName=_methodName;
        this.methodAccess=_methodAccess;
        this.requided=_required;
        this.methodAction=_methodAction;
        this.methodActive=_methodActive;

    }
     public OMethodsInstance copy()
    {
        // Para la copia profunda, clonamos la instancia
        return new OMethodsInstance(this.methodName,methodAccess,requided,methodAction,methodActive);
    }
}
