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
    [SerializeField] private Button RegisterButton;
    [SerializeField] private Button SigninButton;

    private UsersManager usersManager;

    private void Start()
    {
        usersManager = gameObject.GetComponent<UsersManager>();

        RegisterButton.onClick.AddListener(Register);
        SigninButton.onClick.AddListener(Signin);
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
                SceneManager.LoadScene("SinglePlayerScene");
            }
        }

        Debug.Log("user not found");
    }
}
