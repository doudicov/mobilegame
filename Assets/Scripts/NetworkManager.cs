using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager _instance;
    private string serverUrl = "http://localhost:3000";

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

    public IEnumerator RegisterUser(string username, string email, Action<string> callback)
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

    public IEnumerator LoginUser(string email, Action<string> callback)
    {
        string jsonData = $"{{\"email\":\"{email}\"}}";
        using (UnityWebRequest request = new UnityWebRequest(serverUrl + "/login", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            callback(request.result == UnityWebRequest.Result.Success ? request.downloadHandler.text : request.error);
        }
    }

    public void UpdateScoreIfBetter(int userId, float score)
    {
        StartCoroutine(UpdateScoreCoroutine(userId, score));
    }

    IEnumerator UpdateScoreCoroutine(int userId, float score)
    {
        string url = serverUrl + "/update-score";
        ScoreData data = new ScoreData { id = userId, score = score };
        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score update result: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Failed to update score: " + request.error);
        }
    }

    [System.Serializable]
    class ScoreData
    {
        public int id;
        public float score;
    }

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

}
