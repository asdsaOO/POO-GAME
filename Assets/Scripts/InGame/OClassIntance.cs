using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OClassIntance 
{
    // Start is called before the first frame update
   public string className;
   public string classAccess;

   public bool classActive;
   
   public List<OVarInstance> atributes ;
   public List <OMethodsInstance> methods;

    public string [] constructor;

   public OClassIntance(string _className,string _classAccess, bool _classActive, List<OVarInstance> _atributes, List<OMethodsInstance>_methods, string[] _constructor){

    this.className=_className;
    this.classAccess=_classAccess;
    this.classActive=_classActive;
    this.atributes=_atributes;
    this.methods=_methods;
    this.constructor=_constructor;


   }

   public OClassIntance DeepCopy()
    {
        string newClassName=className;
        string newAccsess=classAccess;
        bool newActive=classActive;
        string[] newConstructor=constructor;



        List<OVarInstance> newAtributes = new List<OVarInstance>();
        foreach (var atribute in this.atributes)
        {
            newAtributes.Add(atribute.copy()); // Suponiendo que OVarInstance tiene un método Copy
        }

        List<OMethodsInstance> newMethods = new List<OMethodsInstance>();
        foreach (var method in this.methods)
        {
            newMethods.Add(method.copy()); // Suponiendo que OMethodsInstance tiene un método Copy
        }



        //return new OClassIntance(this.className, this.classAccess, this.classActive, newAtributes, newMethods, null);
        return new OClassIntance(newClassName, newAccsess, newActive, newAtributes, newMethods, newConstructor);
    }


   

  

  

   
   
}
