using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowPanelButton:MonoBehaviour
{
    // Start is called before the first frame update
    //public string idPanel;

    private PanelManagment _panelManagment;

    public ShowPanelButton(){

    }
    private void Start() {
        //cache this
        _panelManagment=PanelManagment.Instance;
    }
    public void doShowPanel(string idPanel){
       // Debug.Log("dosh");
        _panelManagment.showPanel(idPanel);


    }
    
}
