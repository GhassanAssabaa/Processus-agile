using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class UsersManager : MonoBehaviour
{
    private string userDataFileName = "/users.csv";
    private string userDataFilePath;

    private string ReadUserData()
    {
        userDataFilePath = Application.streamingAssetsPath + userDataFileName;
        return "";
    }

    public void WriteUserData(User user)
    {
        userDataFilePath = Application.streamingAssetsPath + userDataFileName;

        using (FileStream file = File.Exists(userDataFilePath) ? 
            File.Open(userDataFilePath, FileMode.Append) : File.Open(userDataFilePath, FileMode.Create))
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine(user.toCsvFormat());
            }
        }
    }

    public List<User> GetUsers()
    {
        List<User> users = new List<User>();

        userDataFilePath = Application.streamingAssetsPath + userDataFileName;

        if (!File.Exists(userDataFilePath))
        {
            FileStream file = File.Open(userDataFilePath, FileMode.Create);
            file.Close();
        }

        using (StreamReader file = new StreamReader(userDataFilePath))
        {
            var line = file.ReadLine();
            var values = line.Split(';');

            if (values.Length > 0)
            {
                if (values[0].Length > 0 && values[1].Length > 0)
                {
                    users.Add(new User(values[0], values[1]));
                }
            }
        }
        return users;
    }
}
