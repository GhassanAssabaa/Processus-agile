using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assests.Scripts
{

    public class UsersManager : MonoBehaviour
    {
        private string userDataFileName = "/users.csv";
        private string userDataFilePath;

        private void FindPath()
        {
            userDataFilePath = Application.streamingAssetsPath + userDataFileName;
        }

        public void WriteUserData(User user)
        {
            FindPath();

            using (FileStream file = File.Exists(userDataFilePath) ?
                File.Open(userDataFilePath, FileMode.Append) : File.Open(userDataFilePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    sw.WriteLine(user.toCsvFormat());
                }
            }
        }

        public void ResetUsersData(List<User> users)
        {
            FindPath();

            if (File.Exists(userDataFilePath))
            {
                File.Delete(userDataFilePath);
            }

            foreach (User user in users) {
                WriteUserData(user);
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            FindPath();

            if (!File.Exists(userDataFilePath))
            {
                FileStream file = File.Open(userDataFilePath, FileMode.Create);
                file.Close();
            }

            using (StreamReader file = new StreamReader(userDataFilePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var values = line.Split(';');

                    if (values.Length > 0)
                    {
                        if (values[0].Length > 0 && values[1].Length > 0)
                        {
                            users.Add(new User(values[0], values[1]));
                        }
                    }
                }
            }
            return users;
        }
    }
}
