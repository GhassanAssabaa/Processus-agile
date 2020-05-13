using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string userName;
    public string password;

    public User(string userName, string password)
    {
        this.userName = userName;
        this.password = password;
    }

    public string toJsonFormat()
    {
        return JsonUtility.ToJson(this, true);
    }

    public string toCsvFormat()
    {
        return userName + ";" + password;
    }
}
