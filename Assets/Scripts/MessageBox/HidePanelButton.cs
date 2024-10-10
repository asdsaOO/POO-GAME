using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    private PanelManagment _panel;
    // Start is called before the first frame update
    void Start()
    {
        _panel=PanelManagment.Instance;
        
    }

    // Update is called once per frame
    public void doHidePanel(){

        _panel.hidePanel();
    }
}
