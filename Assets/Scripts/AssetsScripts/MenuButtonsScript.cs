using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class MenuButtonsScript : MonoBehaviour
{
    
    [SerializeField]
    private int lvlbtn;
    public Image activeImg;
    // Start is called before the first frame update

    void Start()
    {
        iniciarBtn();
        
    }
    void Awake() {
        Debug.Log("botones uniciado");
        iniciarBtn();
        
    }


    private void iniciarBtn (){
        //Renderer rend= activeImg.GetComponent<Renderer>();
        if(MenuMng.lvlsActive[lvlbtn]){
            activeImg.color=Color.green;
            GetComponent<Button>().enabled=true;
            Debug.Log($"el botor { lvlbtn} deberia estar activado");
        }else{
            Debug.Log($"el botor { lvlbtn} deberia estar desactivado");
            activeImg.color=Color.black;
            GetComponent<Button>().enabled=false;
        }
        
    }
    public void btnOnclick(){
        paramsInGame.LvlIngame=lvlbtn+1;
        ScenesMng.changeScene(2);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("boton uniciado");
        //iniciarBtn();
        
    }
}
