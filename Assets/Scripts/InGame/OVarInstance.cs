using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVarInstance 
{
    public  string varName;
    public  string varType;

    public string varAccess;
    public string varValue;

    public OVarInstance( string _varName,string _varType,string _varAccess, string _varValue){
        this.varName=_varName;
        this.varType=_varType;
        this.varAccess=_varAccess;
        this.varValue=_varValue;


    }
   public OVarInstance copy()
    {
        // Para la copia profunda, clonamos la instancia
        return new OVarInstance(this.varName,varType,varAccess,varValue);
    }
}
