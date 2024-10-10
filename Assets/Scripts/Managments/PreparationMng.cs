using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class PreparationMng : MonoBehaviour
{
    public TextMeshProUGUI puntageText;
    public static PreparationMng Instance;
    public int puntage=0;
    public GameObject shipObj;
    public  List<ShipPart> weaponParts;
    public  List<ShipPart> shipColor;
    public  List<ShipPart> sampleParts;
    //////////////////////////////
    ///
    public GameObject messagePanel;
    ///////////////////////////////////////////////////////
    ///
    public TMP_InputField playerData;
    GameRules rules = new GameRules();
    public Player player;
    public static List <CreatedObjects> clasesCreatedList;
    public static List <CreatedObjects> interfacesCreatedList;
    //public static List <CreatedObjects> mainCreatedList;
    public static List <InstanceCreatedObjects> mainClassInstancedList;
    //public static List<OClassIntance> classList;    
    //public bool active =false ;
    public  static bool[] active={false,false,false,false,false,false,false,false} ;
    private void Awake() {
        
        if(Instance!=null&&Instance!=this){
            Debug.Log("se destruyee");
            this.gameObject.SetActive(false);
            /*Destroy(this);
            Destroy(this.gameObject);*/
            
            //DontDestroyOnLoad(Instance);
        }else{
            
            Instance=this;
            //inicializar
            clasesCreatedList= new List<CreatedObjects>();
            interfacesCreatedList= new List<CreatedObjects>();
        //mainCreatedList = new List<CreatedObjects>();
            mainClassInstancedList=new List<InstanceCreatedObjects>();
            iniciarvariables();
            Debug.Log("listas reiniciadas");
            DontDestroyOnLoad(Instance);

        }

        //classList=new List<OClassIntance>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(paramsInGame.LvlIngame);   
    }
    // Update is called once per frame
    void Update()
    {
        
        puntage=calculatePuntage();
        puntageText.text="Puntaje: "+puntage;
       StartCoroutine("updatePlayerData");
       
    }
    private void iniciarvariables(){
        for(int i=0;i<paramsInGame.LvlIngame;i++){
            active[i]=true;
        }
        player= rules.createPlayer(mainClassInstancedList);
        playerData.text=($"Nombre Nave: {player.shipName}\nSalud: {player.health}\nDaño Sec: {player.secondaryDamage}\nDaño Prim: {player.primaryDamage}\nInmunidad: {player.inmunity}");
       
    }
    public void backBtn(){
        Destroy(this);
        Destroy(this.gameObject);
        ScenesMng.changeScene(1);
    }
    public void runBtn(){
        if(paramsInGame.LvlIngame==1 && PreparationMng.mainClassInstancedList.Count>3){
            ResultMessage.saveGame("victory",paramsInGame.LvlIngame,PreparationMng.Instance.puntage);
            Instantiate(messagePanel,transform);
            ScenesMng.changeScene(1);
        }else if(paramsInGame.LvlIngame==1 && PreparationMng.mainClassInstancedList.Count<=3){
            NormalMessageScript messageControll= messagePanel.GetComponent<NormalMessageScript>();
            messageControll.messageText.text="DEBES CREAR POR LO MENOS 4 PARTES DE LA NAVE";
            Instantiate(messagePanel,transform);
        }else{
            if(!PreparationMng.mainClassInstancedList.Any(obj=>obj.classVar.className=="Nave")){
                NormalMessageScript messageControll= messagePanel.GetComponent<NormalMessageScript>();
                messageControll.messageText.text="DEBES CREAR POR LO MENOS LA PARTE PRINCIPAL NAVE";
                Instantiate(messagePanel,transform);
            }else{
                  SceneManager.LoadScene("BattleScene");

            }
        }
        //ScenesMng.changeScene("");
    }
    IEnumerator updatePlayerData (){

        player=rules.createPlayer(mainClassInstancedList);
        

        playerData.text=($"Nombre Nave: {player.shipName}\nSalud: {player.health}\nDaño Sec: {player.secondaryDamage}\nDaño Prim: {player.primaryDamage}\nInmunidad: {player.inmunity}");
        updateShipPArts();
        //
    
        yield return new WaitForSeconds(1);
    }


    private static void updateShipPArts(){
        
        deleteParts();
         int weaponCount=1;

        foreach(var a in mainClassInstancedList ){
           
            if(a.classVar.className=="Nave"){

                OVarInstance varTemp=(from obj in a.classVar.atributes where obj.varName=="color" select obj).First();
                foreach(var b in Instance.shipColor){
                    if(b.partName==varTemp.varValue){
                        b.part.SetActive(true);

                    }else{
                        b.part.SetActive(false);

                        //Debug.Log($" nave no es {b.partName}");
                    }

                }

            }else if(a.classVar.className=="ArmaSecundaria"){
                OVarInstance varTemp =(from obj in a.classVar.atributes where obj.varName=="tipo" select obj).First();
                //Debug.Log(varTemp.varValue);
                foreach(var b in Instance.weaponParts){

                    if(b.partName==varTemp.varValue+weaponCount){
                        b.part.SetActive(true);

                    }
                }

                weaponCount++;
            }else{

                foreach (var b in Instance.sampleParts){
                    
                    if(b.partName== a.classVar.className){
                        b.part.SetActive(true);

                    }else{
                        //Debug.Log(b.partName+" no es igual "+ a.classVar.className);
                    }
                }
            }
        }
    }
    private static void deleteParts(){
        foreach(var a in Instance.shipColor){
            a.part.SetActive(false);
        }
        foreach(var a in Instance.sampleParts){
            a.part.SetActive(false);
        }
        foreach(var a in Instance.weaponParts){
            a.part.SetActive(false);
        }
    }
    private int calculatePuntage(){
        int puntageTemp=0;

        puntageTemp += clasesCreatedList.Count*10;
        puntageTemp+=mainClassInstancedList.Count*15;
        puntageTemp+=interfacesCreatedList.Count*20;
        return puntageTemp;
    }
    
   
}
