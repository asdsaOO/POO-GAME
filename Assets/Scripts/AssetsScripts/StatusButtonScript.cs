using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StatusButtonScript : MonoBehaviour
{
    public Sprite status0Img;
    public Sprite status1Img;
    public bool StatusButton;
    // Start is called before the first frame update
    void Start()
    {
        StatusButton=false;
    }    
    public void onclick_status_btn(){
        UnityEngine.UI.Image imgObj = GetComponent<UnityEngine.UI.Image>();

        if(StatusButton==true){
            imgObj.sprite=status0Img;
            StatusButton=false;
            //return StatusButton;

        }else{
            imgObj.sprite=status1Img;
            StatusButton=true;
            //return StatusButton;
        }

    }

}
