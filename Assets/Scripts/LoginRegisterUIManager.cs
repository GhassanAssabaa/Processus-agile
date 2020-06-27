using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace Assests.Scripts
{
    [RequireComponent(typeof(UsersManager))]
    public class LoginRegisterUIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI userName;
        [SerializeField] private TextMeshProUGUI password;
        [SerializeField] private Button registerButton;
        [SerializeField] private Button signinButton;
        [SerializeField] private Button playAsGuestButton;

        private UsersManager usersManager;
		//Start the game en cliquant sur n'importe quel bouton après la vérification des conditions biensur^^
        private void Start()
        {
            usersManager = gameObject.GetComponent<UsersManager>();

			//Pour register
            registerButton.onClick.AddListener(Register);
			//se connecter et commencer le jeu
            signinButton.onClick.AddListener(Signin);
			//Jouer en tant qu'invité
            playAsGuestButton.onClick.AddListener(PlayAsGuest);
        }
		//Pour s'inscrire
        public void Register()
        {
            //On donne en paramètre username et le mdp
			User user = new User(userName.text, password.text);
			//Ils sont ensuite sauvegarder dans le fichier csv
            usersManager.WriteUserData(user);
        }

        public void Signin()
        {
            //On stocke le ficheir dans une liste
			List<User> users = usersManager.GetUsers();
			//On check si il existe dans la liste pour commencer
            foreach (User user in users)
            {
                if (user.userName == userName.text && user.password == password.text)
                {
                    Debug.Log("Found user");
                    GameManager.currentUser = user;
                    SceneLoader.LoadLevel(1);
                    return;
                }
            }

            Debug.Log("user not found");
        }

        public void PlayAsGuest()
		//Jouer directement et le user a une valeur nulle
        {
            GameManager.currentUser = null;
            SceneLoader.LoadLevel(1);
        }
    }
}
