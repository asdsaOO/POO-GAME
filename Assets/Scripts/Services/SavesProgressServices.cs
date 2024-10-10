using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class SavesProgressServices : MonoBehaviour
{
    public static async Task<string> getProgress(User user){

        StringContent content = new StringContent(JsonConvert.SerializeObject(user));
        using(HttpClient cli = new HttpClient()){
            HttpResponseMessage response = await cli.PostAsync($"{NetworkData.url}/account/getProgress",content);
            //no olvidar esperar a que se lea de forma asyncrona

            var read= await response.Content.ReadAsStringAsync();
            Debug.Log(read.ToString());
            return read.ToString();
        }

    }

    public static async Task<string> updateCloudProgress(User user, Progress progress) {
        var dataProgres= new {
            email=user.email,
            progress=progress
        } ;


        StringContent content = new StringContent(JsonConvert.SerializeObject(dataProgres));
        using(HttpClient cli = new HttpClient()){

            HttpResponseMessage response = await cli.PostAsync ($"{NetworkData.url}/account/updateProgress",content);
            var read = await response.Content.ReadAsStringAsync();
            Debug.Log (read.ToString());
            return read.ToString();
            
        }

    }
}
