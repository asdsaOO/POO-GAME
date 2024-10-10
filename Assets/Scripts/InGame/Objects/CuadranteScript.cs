using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuadranteScript : MonoBehaviour
{
    public EnemyScript enemy;
    public int index;
    public bool activado;
    
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("player")||other.CompareTag("proyectil")||other.CompareTag("enemy")){
            activado=true;
            //Debug.Log("ENTER: hay un objeto detectado "+index);
            enemy.vectorCuadrantes[index]=true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("player")||other.CompareTag("proyectil")||other.CompareTag("enemy")){
            activado=false;
            //Debug.Log("EXIT: salio "+index);
            enemy.vectorCuadrantes[index]=false;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("player")||other.CompareTag("proyectil")||other.CompareTag("enemy")){
            activado=true;
            //Debug.Log("STAY: hay un objeto detectado "+index);
            enemy.vectorCuadrantes[index]=true;
        }
    }
    
}
