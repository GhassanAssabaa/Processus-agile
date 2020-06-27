using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assests.Scripts
{

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button singlePlayerButton;
        [SerializeField] private Button twoPlayerButton;

        private void Start()
        {
            singlePlayerButton.onClick.AddListener(LoadSinglePlayer);
            twoPlayerButton.onClick.AddListener(LoadTwoPlayer);
        }

        private void LoadSinglePlayer()
        {
            SceneManager.LoadScene("LoginRegister");
        }

		// mettre en paramètre 2 hada howa lplayercount qui donne la condition pour que le gameManager idir le jeu pour deux joueurs
        private void LoadTwoPlayer()
        {
            //SceneManager.LoadScene("TwoPlayerScene");
            SceneLoader.LoadLevel(2);
        }
    }
}