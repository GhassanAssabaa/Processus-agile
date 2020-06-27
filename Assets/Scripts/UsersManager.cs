using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assests.Scripts
{

    public class UsersManager : MonoBehaviour
    {
        // Classe qui gère la gestion des users
		private string userDataFileName = "/users.csv";
        private string userDataFilePath;
		
		//Méthode qui cherche le path du fichier CSV
        private void FindPath()
        {
            userDataFilePath = Application.streamingAssetsPath + userDataFileName;
        }
		
		//Méthode qui stocke le user dans le fichier CSV
        public void WriteUserData(User user)
        {
            FindPath();
			
            using (FileStream file = File.Exists(userDataFilePath) ?
                //To open the file
				File.Open(userDataFilePath, FileMode.Append) : File.Open(userDataFilePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(file))
                {
                    //Add user in the file
					sw.WriteLine(user.toCsvFormat());
                }
            }
        }

        public void ResetUsersData(List<User> users)
        {
            FindPath();
			// Test if the file exist
            if (File.Exists(userDataFilePath))
            {
                File.Delete(userDataFilePath);
            }
			//Write the users in the csv file after the test
            foreach (User user in users) {
                WriteUserData(user);
            }
        }

		//Get all the users in the csv file in a list to store them
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
