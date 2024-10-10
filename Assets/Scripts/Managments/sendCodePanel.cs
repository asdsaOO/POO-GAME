using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;

public class sendCodePanel : loginPanelMng
{
    public TextMeshProUGUI codePanelStatus;
    public TMP_InputField codeTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public async void sendCodaBtn(){
        //Debug.Log(userName.text+" "+pass.text);
        var resp = await SignInService.activateAccountService(userName.text,pass.text,Int32.Parse(codeTxt.text));
        var respObject=JsonConvert.DeserializeObject<StatusResponseData>(resp);

        if(respObject.description!="succesfull"){
            codePanelStatus.SetText("codigo incorrecto");


        }else {

            codePanelStatus.SetText("ingresando...");
            //crear credencial local
            User user = new User(userName.text,respObject.userData,pass.text);
            localAccount.createUserCredential(user);
            //cargar datos de progreso
            await  Task.Run(async()=>{
                 await localDataSave.loadProgress();
            });
            //cambiar escena
            ScenesMng.changeScene(1);
        }

    }
}
