using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader
{
    private string mainMenu = "MainScene";
    private string loginRegister = "LoginRegister";

    public void ReloadScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void LoadMainMenu()
    {
        Application.LoadLevel(mainMenu);
    }

    public void LoadSignin()
    {
        Application.LoadLevel(loginRegister);
    }
}
