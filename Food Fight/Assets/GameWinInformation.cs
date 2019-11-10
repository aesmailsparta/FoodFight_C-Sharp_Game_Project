using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinInformation : MonoBehaviour
{

    public float TimeToCompleteLevel;
    public int Score;

    public static GameWinInformation instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }
}
