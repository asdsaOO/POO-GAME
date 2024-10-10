using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameRules : GameClasses
{
    // Start is called before the first frame update
    private List<OClassIntance> oneIstancesClasses = new List<OClassIntance>{
        ship,
        shield,
        engines
    };
    private List<OClassIntance> twoInstancesClasses = new List<OClassIntance>{
        secondaryWeapon
    };
    public void verifyRules (List<InstanceCreatedObjects> instanced, InstanceCreatedObjects newInstance){

        foreach (var i in instanced){
            if(oneIstancesClasses.Any(obj=>obj.className==newInstance.classVar.className)&&instanced.Count(obj=>obj.classVar.className==newInstance.classVar.className)>=1){
                Debug.Log($"nueva isntancia {newInstance.classVar.className}");
                throw new Exception ($"ERROR: La clase {i.classVar.className} ya se creo 1 vez, lo permitido");

            }else if(twoInstancesClasses.Any(obj=>obj.className==newInstance.classVar.className)&&instanced.Count(obj=>obj.classVar.className==newInstance.classVar.className)>=2){
                throw new Exception ($"ERROR: La clase {i.classVar.className} ya se creo  2 vez que es lo permitido para esta clase");
            }
        }
    }

    public Player createPlayer (List<InstanceCreatedObjects> instanced){
        string shipName="";
        int health=0;
        int shieldHealth=0;
        int secondaryDamage=0;
        int primaryDamage=0;
        int inmunity=0;
        foreach(var varInstanced in instanced){
            switch (varInstanced.classVar.className){
                case "Nave":
                foreach(var a in varInstanced.classVar.atributes){
                    if(a.varName=="salud"){
                        health=Convert.ToInt32(a.varValue);

                    }else if(a.varName=="nombre"){
                        shipName=a.varValue;
                    }
                }
                break;
                case "ArmaSecundaria":
                foreach(var a in varInstanced.classVar.atributes){
                    if(a.varName=="daño"){
                        secondaryDamage=secondaryDamage+Convert.ToInt32(a.varValue);

                    }else if(a.varName=="blindaje"){
                        inmunity= (a.varValue=="true")? inmunity=inmunity +20: inmunity;
                    }
                }
                break;

                case "ArmaPrincipal":

                foreach(var a in varInstanced.classVar.atributes){
                    if(a.varName=="daño"){
                        primaryDamage=Convert.ToInt32(a.varValue);
                    }
                }
                break;

                case "Escudo":
                foreach(var a in varInstanced.classVar.atributes){
                    if(a.varName=="salud"){
                        shieldHealth= Convert.ToInt32(a.varValue);
                    }
                }

                break;
                case "Motor":
                foreach (var a in varInstanced.classVar.atributes){
                    if(a.varName=="blindaje"){
                         secondaryDamage= (a.varValue=="true")? inmunity=inmunity +30: inmunity;
                    }
                }
                break;
            }
        }
        return new Player(shipName,health,shieldHealth,secondaryDamage,primaryDamage,inmunity);
    }
}
