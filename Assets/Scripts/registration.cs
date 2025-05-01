using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_Text resultText;

    public void RegisterUser()
    {
        string username = usernameInput.text;
        string email = emailInput.text;

        StartCoroutine(NetworkManager.Instance.RegisterUser(username, email, (response) =>
        {
            resultText.text = "Register Success: ";
            Debug.Log("Register Success: " + response);
        }));
    }
}
