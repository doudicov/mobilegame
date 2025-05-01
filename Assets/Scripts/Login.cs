using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_Text resultText;

    [System.Serializable]
    public class User
    {
        public int id;
        public string username;
        public string email;
        public float score;
    }

    [System.Serializable]
    public class LoginResponse
    {
        public string message;
        public User user;
    }

    public void LoginUser()
    {
        string email = emailInput.text;

        StartCoroutine(NetworkManager.Instance.LoginUser(email, (response) =>
        {
            if (!response.Contains("error"))
            {
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(response);

                PlayerPrefs.SetInt("UserID", loginResponse.user.id);
                PlayerPrefs.Save();

                resultText.text = "Login Successful!";
                SceneManager.LoadScene("GamePlay Scene");
            }
            else
            {
                resultText.text = "Login Failed: " + response;
            }

            Debug.Log("Login Response: " + response);
        }));
    }
}

