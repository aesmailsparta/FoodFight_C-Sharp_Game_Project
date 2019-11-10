using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResults : MonoBehaviour
{

    public Text Time, Score;
    // Start is called before the first frame update
    void Start()
    {

        float timeMS = PlayerPrefs.GetFloat("LevelTime");

        int timeS = (int)timeMS;

        int timeM = timeS / 60;

        int timeH = timeM / 60;

        Time.text = $"{timeH.ToString("D2")} : {timeM.ToString("D2")} : {timeS.ToString("D2")}";


        Score.text = PlayerPrefs.GetInt("LevelScore").ToString();


    }

}
