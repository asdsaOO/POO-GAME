using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using UnityEngine;

public class SignInService : MonoBehaviour
{
    // Start is called before the first frame update
    public static async Task<StatusResponseData> authInService(string emailtxt, string passwordtxt){

        var userData = new  {
            email=emailtxt,
            password=passwordtxt
        };
        
        //Debug.Log(userData.email);

        StringContent content= new StringContent(JsonConvert.SerializeObject(userData));
        //json utility solo funciona con objectos serializables, por ello se utilizara otro metodo para serializar y convertir
        //a formato json
        Debug.Log(JsonConvert.SerializeObject(userData));
        using (HttpClient cli= new HttpClient()){
            HttpResponseMessage response = await cli.PostAsync($"{NetworkData.url}/account/signIn",content);
            Debug.Log($"{NetworkData.url}/account/signIn");

            var responseRead=  await response.Content.ReadAsStringAsync();

            //Debug.Log(responseRead.ToString());
            return JsonConvert.DeserializeObject<StatusResponseData> (responseRead.ToString());

        }
        //return "s";
    }

    public static async Task<string> activateAccountService (string emailtxt,string passwordtxt,int codetxt){

        var userData=new{
            email=emailtxt,
            password=passwordtxt,
            code=codetxt
        };
        StringContent content= new StringContent(JsonConvert.SerializeObject(userData));
        using(HttpClient cli=new HttpClient()){
            HttpResponseMessage response= await cli.PostAsync($"{NetworkData.url}/account/activeAccount",content);
            Debug.Log($"{NetworkData.url}/account/activeAccount");
            var respRead = await response.Content.ReadAsStringAsync();
            Debug.Log(respRead.ToString());
            return respRead.ToString();

        }

    }
}
