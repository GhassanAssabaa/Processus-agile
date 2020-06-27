using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assests.Scripts
{
    [Serializable]
	//Creation de la classe user 
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
		// ToString qui stocke le user dans un string avec un séparateur csv 
        public string toCsvFormat()
        {
            return userName + ";" + password;
        }
    }
}
