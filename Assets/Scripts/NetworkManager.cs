using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;
    private string serverUrl = "http://localhost:3000"; // Change to your local IP for mobile testing

    public static NetworkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("NetworkManager");
                _instance = obj.AddComponent<NetworkManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    // Register a new user
    public IEnumerator RegisterUser(string username, string email, System.Action<string> callback)
    {
        string jsonData = $"{{\"username\":\"{username}\", \"email\":\"{email}\"}}";
        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/register", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            callback(request.result == UnityWebRequest.Result.Success ? request.downloadHandler.text : request.error);
        }
    }
    //to register new user

    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public TMP_Text resultText;

    public void RegisterUser()
    {
        string username = usernameInput.text;
        string email = emailInput.text;

        StartCoroutine(NetworkManager.Instance.RegisterUser(username, email, (response) =>
        {
            resultText.text = "Register Success";
            Debug.Log("Register Success");
        }));
    }
}
