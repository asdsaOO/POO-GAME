using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class paramsInGame : GameClasses
{
    // Start is called before the first frame update
    public static int LvlIngame=1;

    public static List<string> accesInstance= new List<string>{

        "private",
        "protected",
        "public"
        
        
    };

    public static List <string> typeVarInstance = new List<string>(){
        "int",
        "string",
        "bool",
        //"class"
    };

    

    //creamos la lista con las clases que creamos en Gameclasses, esta se usara directamente en el juego
    public static List<OClassIntance>  gameClassessActive = new List<OClassIntance>{
        ship,
        secondaryWeapon,
        shield,
        MainWeapon,
        engines
        
    };

    public static List<OClassIntance> gameInterfacesActive = new List<OClassIntance>{
        GameInterfaces.Ireparable,
        GameInterfaces.Isustituible

    };
    
    




    /*public static List<string> typeMethodInstance = new List<string>(){
        "private",
        "public",
        "static",



    };*/


}
