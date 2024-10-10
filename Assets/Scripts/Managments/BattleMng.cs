using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Assertions.Must;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;

public class BattleMng : MonoBehaviour
{
    private bool playerMoveAcive = true, playerActionActive = true;
    public TMP_InputField buttonSelectedTxt;
    public List<ButtonConfig> buttonList;
    public Button playbtn;
    private string buttonSelectedVal=null;
    public  bool playStatus=false;//si es true entonces es play para iniciar el juego
    bool gameStop =false;//si es true entocnes hay un ganador
    public TextMeshProUGUI ScoreTxt;
    public GameObject victoryMessage;    

    public GameObject demoship;
    public GameObject playerLifeBar;
    public GameObject enemyLifebar;

    public TMP_InputField codeTxt;
    public TMP_InputField debug;
    
    public GameObject playerShipSpace;
    public GameObject enemyShip;

    public PlayerScript playerController;
    public EnemyScript enemyController;
    ///------------------SINGLETON---------------------
    public static BattleMng instance;
    private void Awake() {
        //shipAsset= new GameObject("");
    }
    void Start()
    {
         if(instance!=null&&instance!=this){
            Destroy(this);

         }else{
            instance=this;
         }
        
        //inicializar botones
        initButtons();
        ///
        ScoreTxt.text="Puntaje: "+PreparationMng.Instance.puntage;
        playerController= playerShipSpace.GetComponent<PlayerScript>();
        enemyController=enemyShip.GetComponent<EnemyScript>();      
        Instantiate(PreparationMng.Instance.shipObj,playerShipSpace.transform);
        ///-------------------------DEMODATA--------------------------------------
        //Instantiate (demoship,playerShipSpace.transform);
        //Debug.Log(PreparationMng.clasesCreatedList[0].classVar.className); 
    }
    // Update is called once per frame
    void Update()
    {
       updatePlayStatus();
       playerLifeBar.GetComponent<LifebarScript>().actualLife=playerShipSpace.GetComponent<PlayerScript>().playerData.health;
       playerLifeBar.GetComponent<LifebarScript>().actualShield=playerShipSpace.GetComponent<PlayerScript>().playerData.shield;

       enemyLifebar.GetComponent<LifebarScript>().actualLife=enemyShip.GetComponent<EnemyScript>().playerData.health;
       enemyLifebar.GetComponent<LifebarScript>().actualShield=enemyShip.GetComponent<EnemyScript>().playerData.shield;
       onPressButton();
       if(gameStop==false){
        findWinner();
       }   
    }
    public void runConsoleBtn(){
        try {
            var items = AmalyceMainConsole.analyceBattleConsole( ConsolesText.mapFormatText(codeTxt.text.Replace('\n',' ')));
            Debug.Log($"la accion es {items.Item1} y la clase {items.Item2.className}");
            saveButtonData(items.Item1,(codeTxt.text).Trim(),items.Item2);

            //doMov(items.Item1,items.Item2);
        }catch(Exception e){
            Debug.Log(e);
            debug.text= e.Message;
        }
    }
    private  void doMov (string mov, OClassIntance classVar){
        switch(classVar.className){
            case "Nave":
            if(mov=="reparar"){
                playerController.repair(classVar.className);
            }
            break;
            case "ArmaSecundaria":
            string tipo =(from obj in classVar.atributes where obj.varName=="tipo" select obj.varValue ).First();
            Debug.Log("el tipo es "+tipo);
            playerController.shootPjectile(tipo,classVar.className);
           
            break;
            case "ArmaPrincipal":
            playerController.shootPjectile("plasma","ArmaPrincipal");
            break;
            case "Motor":
            if(mov=="moverDerecha"){
                if(playerMoveAcive){
                    playerController.moveShip("right");
                    StartCoroutine("coldownMov");
                }else{
                    Debug.Log("esta en carga");
                }
            }else{
                if(playerMoveAcive){
                    playerController.moveShip("left");
                    StartCoroutine("coldownMov");
                }else{
                    Debug.Log("esta en carga");
                }
            }
            if(mov=="sustituir"){
            }
            break;
            case "Escudo":
            if(mov=="reparar"){
                playerController.repair(classVar.className);
            }
            break;
        }        
    }
    private void findWinner (){
        //Debug.Log("la vida del enemigo es "+enemyController.playerData.health);
        if(playerController.playerData.health<=0){   
            ResultMessage mnsControl = victoryMessage.GetComponent<ResultMessage>();
            mnsControl.resultText.text="Derrota";
            mnsControl.score.text=PreparationMng.Instance.puntage.ToString();
            Instantiate(victoryMessage,transform);
            gameStop=true;
        }else if(enemyController.playerData.health<=0){
            PreparationMng.Instance.puntage+=50;
            ResultMessage mnsControl = victoryMessage.GetComponent<ResultMessage>();
            mnsControl.resultText.text="victory";
            mnsControl.score.text=PreparationMng.Instance.puntage.ToString();
            Instantiate(victoryMessage,transform);
            gameStop=true;
        }
    }
    private void updatePlayStatus(){
        if(playbtn.GetComponent<StatusButtonScript>().StatusButton!=playStatus){
            playStatus=playbtn.GetComponent<StatusButtonScript>().StatusButton;
            Debug.Log("el juego esta en"+ playStatus);

        }
    }
    private void initButtons(){
        foreach(var a in buttonList){
            a.btn.onClick.AddListener(()=>onclickControllButtons(a));
        }
    }
    private void onclickControllButtons(ButtonConfig bntcnf){
        buttonSelectedTxt.text="//seleccionado: "+bntcnf.value;
        buttonSelectedVal=bntcnf.value;
        codeTxt.text=bntcnf.code;
        Debug.Log("boton seleccionado: "+buttonSelectedVal+" accion:"+bntcnf.action);
    }
    private void saveButtonData(string action, string code, OClassIntance classvar){
        ButtonConfig btnconf = (from obj in buttonList where obj.value == buttonSelectedVal select obj).First();
        btnconf.action=action;
        btnconf.code=code;
        btnconf.classVar=classvar;
    }
    private void onPressButton(){
        if(playStatus){
        if(Input.GetKeyDown(KeyCode.A)){
            ButtonConfig btn =(from obj in buttonList where obj.value == "a" select obj).First();
            if(btn.action!=null && btn.action!=""){
                doMov(btn.action,btn.classVar);
            }else{
                Debug.Log("no tiene asignada accion");
            }
        }else if(Input.GetKeyDown(KeyCode.D)){
            ButtonConfig btn =(from obj in buttonList where obj.value == "d" select obj).First();
            if(btn.action!=null && btn.action!=""){
                doMov(btn.action,btn.classVar);
            }else{
                Debug.Log("no tiene asignada accion");
            }
        }else if(Input.GetKeyDown(KeyCode.Q)){
            ButtonConfig btn =(from obj in buttonList where obj.value == "q" select obj).First();
            if(btn.action!=null && btn.action!=""){
                doMov(btn.action,btn.classVar);
            }else{
                Debug.Log("no tiene asignada accion");
            }
        }else if(Input.GetKeyDown(KeyCode.W)){
            ButtonConfig btn =(from obj in buttonList where obj.value == "w" select obj).First();
            if(btn.action!=null && btn.action!=""){
                doMov(btn.action,btn.classVar);
            }else{
                Debug.Log("no tiene asignada accion");
            }
        }else if(Input.GetKeyDown(KeyCode.E)){
            ButtonConfig btn =(from obj in buttonList where obj.value == "e" select obj).First();
            if(btn.action!=null && btn.action!=""){
                doMov(btn.action,btn.classVar);
            }else{
                Debug.Log("no tiene asignada accion");
            }
        }
        }
    }
    IEnumerator coldownMov(){
        playerMoveAcive=false;
        yield return new WaitForSeconds(0.5f);
        playerMoveAcive=true;
    }
    public static void backScene (){
        SceneManager.LoadScene(2);
    }
}
