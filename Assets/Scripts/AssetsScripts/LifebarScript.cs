using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifebarScript : MonoBehaviour
{
    
    public float lifeMax=100f;
    public float shieldMac=100f;

    public float actualLife;
    public float actualShield;

    public Image lifeIndicator;
    public Image shielIndicator;

    public TextMeshProUGUI numeralIndicator;
    // Start is called before the first frame update
    void Start()
    {
        actualLife=100f;
        actualShield=100f;
        
        
    }

    // Update is called once per frame
    void Update()
    {
       lifeIndicator.fillAmount=actualLife/lifeMax;
       shielIndicator.fillAmount=actualShield/shieldMac;
       numeralIndicator.SetText(actualLife.ToString());

       
        

        
    }

   
}
