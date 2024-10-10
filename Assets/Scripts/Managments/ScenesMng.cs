using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMng : MonoBehaviour
{
    
    public  static void changeScene(int index){

        SceneManager.LoadScene(index);
        /*if(index==0){
             SceneManager.UnloadScene(1);
             //SceneManager.UnloadSceneAsync(2);

            SceneManager.LoadScene(index);
            
        }else{
            SceneManager.LoadScene(index);
        }*/
        

    }

    public  static void changeSceneLoad(int index){

        SceneManager.LoadScene(index);
        /*if(index==0){
             SceneManager.UnloadScene(1);
             //SceneManager.UnloadSceneAsync(2);

            SceneManager.LoadScene(index);
            
        }else{
            SceneManager.LoadScene(index);
        }*/
        

    }
}
