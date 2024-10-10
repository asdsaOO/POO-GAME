using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
[Serializable]
public class GameModel 
{
    public string result;
    public int numGame;
    public int score;
    public bool useClass;
    public bool useConstr;
    public bool useInterface;
    public bool useAbstractClass;
    public bool usePrivate;
    
    public bool useProtected;

    public GameModel(string _result, int _numGame, int _score, bool _useClass, bool _useConstr,
                    bool _useInterface,bool _useAbstractClass,bool _usePrivate,bool _useProtected){
        result = _result;
        numGame = _numGame;
        score = _score;
        useClass = _useClass;
        useConstr = _useConstr;
        useInterface = _useInterface;
        useAbstractClass = _useAbstractClass;
        usePrivate=_usePrivate;
        useProtected=_useProtected;
    }
}
