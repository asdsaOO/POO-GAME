using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NormalMessageScript : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onlyDestroy(){

        Destroy(this.gameObject);
    }

    public void onlyLvl1(){

        
    }
}
