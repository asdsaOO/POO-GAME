using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;


public class SignUpService : MonoBehaviour
{

     public static async Task<bool> createUser(User user){
        //Debug.Log((JsonUtility.ToJson(user)).ToString());
        StringContent content= new StringContent(JsonUtility.ToJson(user));

        using(HttpClient cli = new HttpClient()){
            

            HttpResponseMessage respose = await cli.PostAsync($"{NetworkData.url}/account/signUp",content);

            var responseRead=   await respose.Content.ReadAsStringAsync();

            
            Debug.Log(responseRead);

            return true;
           
        }
        

        
    }

    
    // Start is called before the first frame update

}
