using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour
{
    public TMP_Text finalTimeText;
    public TMP_Text bestTimeText;

    void Start()
    {
        float finalTime = Timer.Instance.GetElapsedTime();
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (finalTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", finalTime);
            bestTime = finalTime;

            // Save best score to backend
            int userId = PlayerPrefs.GetInt("UserID", -1);
            if (userId != -1)
            {
                NetworkManager.Instance.UpdateScoreIfBetter(userId, finalTime);
            }
        }

        finalTimeText.text = FormatTime(finalTime);   // Shows only 00:19
        bestTimeText.text = FormatTime(bestTime);     // Shows only 00:17
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


