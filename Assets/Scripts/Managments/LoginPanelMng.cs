using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using Unity.VisualScripting.Dependencies.NCalc;
using System.Threading.Tasks;


public class loginPanelMng :MonoBehaviour
{
    private PanelManagment _panelMnanagment;
    public TextMeshProUGUI status;
    public TMP_InputField userName;
    public TMP_InputField pass;

    // Start is called before the first frame update
    void Start()
    {
        _panelMnanagment=PanelManagment.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void ingresarBtn(){

        status.SetText("estableciondo conexion");
        StatusResponseData resp= await SignInService.authInService(userName.text,pass.text);
       
        Debug.Log($"{resp.code} {resp.description}");

        switch(resp.description)
        {
            case "denied":
            status.SetText("acceso denegado");

            break;

            case "non-active":
            status.SetText("debe activar");
            _panelMnanagment.showPanel("inputCodePanel");
            break;

            case "succesfull":
            status.SetText("Ingresando...");
            Debug.Log(resp.userData);
            //crear credencial como file local
            localAccount.createUserCredential(new User(userName.text,resp.userData,pass.text));
            //cargar datos de progreso de firebase
            
            await localDataSave.loadProgress();
            
            
            ScenesMng.changeScene(1);
            
            break;
        }

    }
    

    
}
