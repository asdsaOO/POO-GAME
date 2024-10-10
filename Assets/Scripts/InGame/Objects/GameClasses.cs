using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClasses
{
    public static OClassIntance ship = new OClassIntance(
       "Nave",
       "editAccess",
       false,
       new List<OVarInstance>{
            new OVarInstance("nombre","string","editAcces",null),
            new OVarInstance("salud","int","editAcces",null),
            new OVarInstance("color","string","editAcces",null)
       },
       new List<OMethodsInstance>{
            new OMethodsInstance("reparar","editAcces",false,"reparar ( ) ;",false),
       }
       ,
       null
   );

    public static OClassIntance secondaryWeapon = new OClassIntance(

        "ArmaSecundaria",
        "editAccess",
        false,
        new List<OVarInstance>{
            new OVarInstance("tipo","string","editAcces",null),
            new OVarInstance("blindaje","bool","editAcces",null),
            new OVarInstance("daño","int","editAccess",null),
            new OVarInstance ("activo","bool","editAccess",null)
        },
        new List<OMethodsInstance>{
            new OMethodsInstance("disparar","editAccess",false,"disparar ( ) ;",false),
            new OMethodsInstance("reemplazar","editAccess",false,"reemplazar ( ) ;",false)
        },
        null
    );
    public static OClassIntance shield = new OClassIntance(
         "Escudo",
         "editAccess",
         false,
         new List<OVarInstance>{
            new OVarInstance("salud","int","editAccess",null),
            new OVarInstance ("activo","bool","editAccess",null)
         },
         new List<OMethodsInstance>{
            new OMethodsInstance("reparar","editAccess",false,"reparar ( ) ;",false)
         },
         null
     );
    public static OClassIntance engines = new OClassIntance(
       "Motor",
       "editAccess",
       false,
       new List<OVarInstance>{
            new OVarInstance ("blindaje","bool","editAccess",null),
            new OVarInstance ("resistenciaBioma","bool","editAccess",null),
            new OVarInstance ("activo","bool","editAccess",null),

       },
       new List<OMethodsInstance>{
            new OMethodsInstance("moverDerecha","editAccess",false,"moverDerecha ( ) ;",false),
            new OMethodsInstance("moverIzquierda","editAccess",false,"moverIzquierda ( ) ;",false),
            new OMethodsInstance("sustituir","editAccess",false,"sustituir ( ) ;",false)
       },
       null
   );
    public static OClassIntance MainWeapon = new OClassIntance(
        "ArmaPrincipal",
        "editAccess",
        false,
        new List<OVarInstance>{
            new OVarInstance("tipo","string","editAcces",null),
            new OVarInstance("daño","int","editAccess",null),
            new OVarInstance ("activo","bool","editAccess",null)

        },
        new List<OMethodsInstance>{
            new OMethodsInstance("disparar","editAccess",false,"disparar ( ) ;",false),
        },
        null
    );

}

