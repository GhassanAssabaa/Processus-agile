using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private void LoadTwoPlayer()
    {
        //SceneManager.LoadScene("TwoPlayerScene");
        SceneLoader.LoadLevel(2);
    }
}
