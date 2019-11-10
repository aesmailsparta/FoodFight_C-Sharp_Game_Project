using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStatistics : MonoBehaviour
{

    public Text BulletsFiredText;
    public Text EnemiesKilledText;
    public Text HighScoreText;
    public Text TotalTimeText;
    public Text LivesLostText;
    // Start is called before the first frame update
    void Awake()
    {
        float time = PlayerPrefs.GetFloat("TotalTime", 0);
        string formattedTime = "";
        if(time == 0)
        {
            formattedTime = "00:00:00";
        }
        else
        {
            int timeS = (int)time;

            int timeM = timeS / 60;

            int timeH = timeM / 60;
            formattedTime = $"{timeH.ToString("D2")} : {timeM.ToString("D2")} : {timeS.ToString("D2")}";
        }

        BulletsFiredText.text = PlayerPrefs.GetInt("BulletsFired", 0).ToString("D7");
        EnemiesKilledText.text = PlayerPrefs.GetInt("EnemiesKilled", 0).ToString("D4");
        HighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString("D7");
        TotalTimeText.text = formattedTime;
        LivesLostText.text = PlayerPrefs.GetInt("LivesLost", 0).ToString("D4");

        this.gameObject.SetActive(false);
    }
}
