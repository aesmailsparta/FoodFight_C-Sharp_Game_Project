using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject StartLocation;

    public Texture2D AimReticle;

    public static int KeysObtained = 0;

    public static GameManager instance;

    public static GameObject EnemyHit;

    public static int BulletsFired = 0;
    public static int EnemiesKilled = 0;
    public static float TotalTime = 0;
    public static float HighScore = 0;

    public Animator KeysUIAnimator;
    private GameObject player;

    public GameObject HUD;
    public GameObject PauseMenu;

    public Slider EnemyHealthBar;
    public Text EnemyHealthLabel;
    public Image EnemyHealthIcon;

    public Transform Shooter;
    public Transform Boss;

    public FixedJoystick joystick;
    public FixedJoystick aimjoystick;

    // public ScoreManager scoreManager;

    public static GameObject UIMessageBox;

    private float LevelTimer;

    public static bool isPaused;
    private bool isLevelFinished = false;

    public Text ScoreText;

    [SerializeField]
    private Text Keys;

    [SerializeField]
    public Vector2 HotSpot;

    void Awake()
    { 
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        StartLocation = GameObject.FindGameObjectWithTag("StartPosition");
        UIMessageBox = GameObject.FindGameObjectWithTag("MessageBox");
        UIMessageBox.SetActive(false);
        player = (GameObject)Instantiate(Player, StartLocation.transform.position, Quaternion.identity);

        if (Keys)
        {
            Keys.text = KeysObtained.ToString();
        }

        PlayerPrefs.SetFloat("LevelTime", 0);
        PlayerPrefs.SetInt("LevelScore", 0);

        BulletsFired = PlayerPrefs.GetInt("BulletsFired", 0);
        EnemiesKilled = PlayerPrefs.GetInt("EnemiesKilled", 0);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        TotalTime = PlayerPrefs.GetFloat("TotalTime", 0);

        SetUpCursor(AimReticle, HotSpot);

        LevelTimer = 0.0f;

        player.GetComponent<PlayerController>().joystick = this.joystick;
        player.GetComponent<PlayerController>().aimjoystick = this.aimjoystick;

    }

    public void Teleport(GameObject go, Transform t)
    {
        go.transform.position = t.position;
    }

    private void SetUpCursor(Texture2D tex, Vector2 Hotspot)
    {
        CursorMode cursorMode = CursorMode.Auto;
        Cursor.SetCursor(tex, Hotspot, cursorMode);
    }

    void Start()
    {
    //    GameWinInformation.instance.TimeToCompleteLevel = 0;
    //    GameWinInformation.instance.Score = 0;
    }

    public float GetTime()
    {
        return LevelTimer;
    }

    public void KeyPickedUp()
    {
        KeysObtained++;
        Keys.text = KeysObtained.ToString();
        KeysUIAnimator.Play("PickedUpKey");
    }

    public void KeysUsed(int KeysUsed)
    {
        KeysObtained -= KeysUsed;
        Keys.text = KeysObtained.ToString();
    }

    public void UpdateScoreUI()
    {
        if (ScoreText)
        {
            ScoreText.text = $"{ScoreManager.score.ToString("D11")}";
        }
    }

    public void UpdateScore(int points)
    {
        ScoreManager.score += points;
        UpdateScoreUI();
    }

    public void UpdateLevelTimer()
    {
        LevelTimer += Time.deltaTime;
    }

    public void SetPauseState()
    {
        isPaused = !isPaused;
        HUD.SetActive(!isPaused);
        PauseMenu.SetActive(isPaused);
    }

    void Update()
    {
        if (!isLevelFinished)
        {
            if (!isPaused)
            {
                UpdateLevelTimer();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                SetPauseState();
                UnityEngine.Debug.Log($"Game is Paused : {isPaused}");
            }
        }

        if(EnemyHit != null)
        {
            UpdateEnemyHealthUI();
        }
        else
        {
            EnemyHealthBar.gameObject.SetActive(false);
            EnemyHealthLabel.gameObject.SetActive(false);
            EnemyHealthIcon.gameObject.SetActive(false);
        }
    }

    public void UpdateEnemyHealthUI()
    {
        if(EnemyHealthBar != null)
        {
            EnemyHealthBar.gameObject.SetActive(true);
            EnemyHealthLabel.gameObject.SetActive(true);
            EnemyHealthIcon.gameObject.SetActive(true);
            Health h = EnemyHit.GetComponent<Health>();
            string EnemyName = h.Name;
            Sprite EnemyIcon = h.Icon;
            float normalizedHealth = h.GetHealth() / h.GetMaxHealth();
            EnemyHealthBar.value = normalizedHealth;
            EnemyHealthLabel.text = EnemyName;
            EnemyHealthIcon.sprite = EnemyIcon;
        }
    }

    public void LevelComplete()
    {
        isLevelFinished = true;

        PlayerPrefs.SetFloat("LevelTime", LevelTimer);
        PlayerPrefs.SetInt("LevelScore", ScoreManager.score);

        TotalTime += LevelTimer;

        PlayerPrefs.SetInt("BulletsFired", BulletsFired);
        PlayerPrefs.SetInt("EnemiesKilled", EnemiesKilled);
        PlayerPrefs.SetFloat("TotalTime", TotalTime);
        if (ScoreManager.score > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", ScoreManager.score);
        }


        SceneManager.LoadScene("Win");
    }
}
