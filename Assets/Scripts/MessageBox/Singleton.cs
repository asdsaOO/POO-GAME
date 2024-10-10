using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    // Start is called before the first frame update
    private static T _instance;
    public static T Instance{

        get{
            return _instance;
        }
    }

    private void Awake() {
        _instance=(T)(object) this;
    }
}
