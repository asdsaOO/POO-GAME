using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class ResultMessage : MonoBehaviour
{
     public TextMeshProUGUI resultText;
     public TextMeshProUGUI score;
    
    private void Start() {
        Debug.Log("el nivel del juego es "+paramsInGame.LvlIngame);
    }
    
    public void okBtn(){
        saveGame(resultText.text,paramsInGame.LvlIngame,Convert.ToInt32( score.text));
        Destroy(this.gameObject);
        ScenesMng.changeScene(1);

    }

    public static void saveGame ( string result, int lvl, int score){
        Progress progress = localDataSave.GetLocalProgress();
        if(result=="victory"){
            
            progress.score+=score;
            switch(lvl){
                case 1:
                progress.win1+=1;
                if(lvl>=progress.lvl){
                    progress.lvl++;

                }
                break;
                case 2:
                progress.win2+=1;
                if(lvl>=progress.lvl){
                    progress.lvl++;

                }
                break;
                case 3:
                progress.win3+=1;
                if(lvl>=progress.lvl){
                    progress.lvl++;

                }
                break;
                case 4:
                progress.win4+=1;
                if(lvl>=progress.lvl){
                    progress.lvl++;

                }
                break;
                case 5:
                progress.win5+=1;
                if(lvl>=progress.lvl){
                    progress.lvl++;
                }
                break;
            }

        }else{
            
            progress.defeat+=1;
            progress.score+=score;
        }
        if(progress.games==null){
            progress.games = new List<GameModel>();

        }
        progress.games.Add(new GameModel(
            result,
            progress.games.Count+1,
            score,
            (PreparationMng.clasesCreatedList.Count >0?true:false),
            (PreparationMng.clasesCreatedList.Any(obj=>obj.classVar.constructor!=null)),
            (PreparationMng.interfacesCreatedList.Count >0?true:false),
            false,
            (PreparationMng.clasesCreatedList.Any(obj=>obj.classVar.atributes.Any(obj2=>obj2.varAccess=="private"))),
            (PreparationMng.clasesCreatedList.Any(obj=>obj.classVar.atributes.Any(obj2=>obj2.varAccess=="protected")))
            //arreglar clases abstractas
        ));
     Debug.Log("nuevo nivel "+progress.lvl);

        localDataSave.overwrite(progress);
        ScenesMng.changeScene(1);

    }
}
