using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class signUpControll : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI status;

    public TMP_InputField email;
    public TMP_InputField userName;
    public TMP_InputField pass;
    public TMP_InputField pass2;
   private void Awake() {
    // status=GameObject.Find("statustxt").GetComponent<TextMeshProUGUI>();
    // email=GameObject.Find("emailUp").GetComponent<TMP_InputField>();
    // userName=GameObject.Find("userUp").GetComponent<TMP_InputField>();
    // pass=GameObject.Find("passUp").GetComponent<TMP_InputField>();
    // pass2=GameObject.Find("pass2Up").GetComponent<TMP_InputField>();
    // userName.text="asdfsadfasdf";
   }
   public async void signUpListener ( ){
    //Debug.Log($"{email.text}, {userName.text}, {pass.text}, {pass2.text}");
    

    if(pass.text==pass2.text&&pass2.text!=""){
        User newUser=  new User(email.text,userName.text,pass.text);
        Debug.Log(email.text);
        status.SetText("creando usuario....");

        var resp= await SignUpService.createUser(newUser);
        
        status.SetText("se envio un codigo de verificacion al correo");
        Debug.Log(resp);

    }else{

        status.SetText("las contraseñas no coinciden");
    }

   }

}
