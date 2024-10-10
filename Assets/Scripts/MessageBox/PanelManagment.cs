using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
public class PanelManagment:Singleton<PanelManagment>
{
    public  List<PanelModel> panels; 
    private Queue<PanelInstanceModel> _queue= new Queue<PanelInstanceModel>();
    public void showPanel(string panelIdvar){
        PanelModel panelModel=panels.FirstOrDefault(panel=>panel.panelId==panelIdvar);
        if(panelModel!=null){
            var newInstancePanel=Instantiate(panelModel.panelPrefab,transform);
            //add the new panel to enqueue
            _queue.Enqueue(new PanelInstanceModel{
                panelId=panelIdvar,
                panelInstance=newInstancePanel
            });

        }else{
            Debug.Log("no panels");
        }
    }
    public void hidePanel(){
        if(_queue.Count>0){
            var last=_queue.Dequeue();
            Destroy(last.panelInstance.gameObject);
        }else{
            Debug.Log($"no hay nada instanciado en esta lista hay  {_queue.Count} elementos");
        }
    }
    // Start is called before the first frame update
}
