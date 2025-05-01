using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneButtons : MonoBehaviour
{
    public void PlayAgain()
    {
        if (Timer.Instance != null)
            Timer.Instance.DestroySelf();

        SceneManager.LoadScene("GamePlay Scene");  
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Login Scene");  
    }
}
