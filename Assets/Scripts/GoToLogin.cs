using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLogin : MonoBehaviour
{
    public void LoadLoginScene()
    {
        SceneManager.LoadScene("Login Scene"); 
    }
}

