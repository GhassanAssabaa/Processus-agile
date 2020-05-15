using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UsersManager))]
public class LoginRegisterUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userName;
    [SerializeField] private TextMeshProUGUI password;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button signinButton;
    [SerializeField] private Button playAsGuestButton;

    private UsersManager usersManager;

    private void Start()
    {
        usersManager = gameObject.GetComponent<UsersManager>();

        registerButton.onClick.AddListener(Register);
        signinButton.onClick.AddListener(Signin);
        playAsGuestButton.onClick.AddListener(PlayAsGuest);
    }

    public void Register()
    {
        User user = new User(userName.text, password.text);
        usersManager.WriteUserData(user);
    }

    public void Signin()
    {
        List<User> users = usersManager.GetUsers();

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
    {
        GameManager.currentUser = null;
        SceneLoader.LoadLevel(1);
    }
}
