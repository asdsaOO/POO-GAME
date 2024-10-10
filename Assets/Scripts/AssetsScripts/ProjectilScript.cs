using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilScript : MonoBehaviour
{

    public string type;
    public int damage=10;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(moveProjectil());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("player")){
            Debug.Log("chocooo daño de impacto "+damage);
            //Destroy(this.gameObject);

        }
        
    }

    public IEnumerator moveProjectil( ){
        float psY=transform.localPosition.y;

        /*float posY=transform.localPosition.y;
        float posX = transform.localPosition.x;*/
        while(true){
            /*
            psY++;
            transform.localPosition=new Vector3(transform.localPosition.x,psY,0);
            yield return new WaitForSeconds (0.0001f);*/
            transform.Translate(Vector3.up * Time.deltaTime * 80f); // Mueve el proyectil hacia arriba
            yield return null;
        }

    }
}
