using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    private SceneLoader loader;

    void Awake()
    {
        gameObject.SetActive(false);
        loader = new SceneLoader();

        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(MainMenu);
    }

    private void Restart()
    {
        loader.ReloadScene();
    }

    private void MainMenu()
    {
        loader.LoadMainMenu();
    }
}
