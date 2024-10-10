using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PcAnimation:MonoBehaviour

{
    [SerializeField]
    private int lvl;


    // Start is called before the first frame update public bool active = true ;
    //public bool active=false;

    //public Image pcImage;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //active=true;
         animator=GetComponent<Animator>();
         //Debug.Log(PreparationMng.active1);
         
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("active",PreparationMng.active[lvl]);
        if(PreparationMng.active[lvl]==false){
            this.gameObject.GetComponent<EventTrigger>().enabled=false;

        }
    }
}
