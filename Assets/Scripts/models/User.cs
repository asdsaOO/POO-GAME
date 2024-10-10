using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    // Start is called before the first frame update
    
    public string email;
    public string userName;

    //public bool status;
    public string password;

    public User(string _email,string _userName,string _password)
    {

        this.email=_email;
        this.userName=_userName;
        //this.status=_status;
        this.password=_password;
    }
}
