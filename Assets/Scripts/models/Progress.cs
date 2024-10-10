using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Progress{

    // Start is called before the first frame update
   public int lvl;
   public int defeat;
   public int score;
   public int win1;
   public int win2;
   public int win3;
   public int win4;
   public int win5;
   public List<GameModel> games= new List<GameModel>();
   public List<RewardModel> rewards= new List<RewardModel>();
   

}
