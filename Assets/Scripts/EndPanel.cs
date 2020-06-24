using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assests.Scripts
{

    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;

        void Awake()
        {
            gameObject.SetActive(false);

            restartButton.onClick.AddListener(Restart);
            menuButton.onClick.AddListener(MainMenu);
        }


        private void Restart()
        {
            SceneLoader.ReloadScene();
        }

        private void MainMenu()
        {
            SceneLoader.LoadMainMenu();
        }
    }
}
