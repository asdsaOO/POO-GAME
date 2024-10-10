using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public string shipName;
    // Start is called before the first frame update
    public int health;
    public int shield;

    public int secondaryDamage;
    public int primaryDamage;

    public int inmunity;


    public Player(string _shipName, int _health, int _shield, int _secondaryDamage, int _primaryDamage, int _inmunity){

        shipName = _shipName;
        health=_health;
        shield=_shield;
        secondaryDamage=_secondaryDamage;
        primaryDamage=_primaryDamage;
        inmunity=_inmunity;

    }
    
}
