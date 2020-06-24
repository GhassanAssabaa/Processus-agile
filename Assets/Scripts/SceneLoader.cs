using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assests.Scripts
{
    public static class SceneLoader
    {
        private static string mainMenu = "MainScene";
        private static string loginRegister = "LoginRegister";
        private static string level = "TestScene";

        public static void ReloadScene()
        {
            GameObject.FindObjectOfType<GameState>().Unpause();
            Application.LoadLevel(Application.loadedLevel);
        }

        public static void LoadMainMenu()
        {
            GameObject.FindObjectOfType<GameState>().Unpause();
            Application.LoadLevel(mainMenu);
        }

        public static void LoadSignin()
        {
            GameObject.FindObjectOfType<GameState>().Unpause();
            Application.LoadLevel(loginRegister);
        }

        public static void LoadLevel(int playerCount)
        {
            if (playerCount > 0 && playerCount < 3)
            {
                GameManager.playerCount = playerCount;
                Application.LoadLevel(level);
            }
        }
    }
}
