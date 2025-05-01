using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOrder : MonoBehaviour
{
    public void GoToSignup()
    {
        SceneManager.LoadScene("Signup Scene"); 
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene("GamePlay Scene");
    }
}

