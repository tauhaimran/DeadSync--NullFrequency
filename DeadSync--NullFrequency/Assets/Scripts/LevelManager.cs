using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net.Http.Headers;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Triggers (in order)")]
    public GameObject[] levelTriggers;

    [Header("Beams (in order)")]
    public GameObject[] levelBeams;

    [Header("UI")]
    public TMP_Text levelText;      
    public TMP_Text pauseText;      // ← drag your pause TMP text

    [Header("Controls")]
    public KeyCode pauseKey = KeyCode.P;
    public KeyCode quitKey = KeyCode.Q;

    private int currentLevel = 0;
    private bool isPaused = false;

    public Slider HP_Slider;
    
    void Start()
    {
        UpdateLevelState();

        if (pauseText != null)
            pauseText.gameObject.SetActive(false);
    }

    void Update()
    {
        HandlePause();
        HandleQuit();
    }

    private void LateUpdate()
    {
        UpdateCoinsTxt();
        UpdateHP();
    }

    public TextMeshProUGUI CoinText;
    void UpdateCoinsTxt() => CoinText.text = SaveData.instance.Coins.ToString();
    void UpdateHP() => HP_Slider.value = Player.instance != null ? Player.instance.hp / Player.instance.MaxHP : 0;

    public void RetryClick()
    {
        SceneManager.LoadScene("MainMap");
    }
    public void GoToMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ========================
    //   PAUSE SYSTEM
    // ========================
    void HandlePause()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            isPaused = !isPaused;

            Time.timeScale = isPaused ? 0f : 1f;

            if (pauseText != null)
                pauseText.gameObject.SetActive(isPaused);

            Debug.Log(isPaused ? "GAME PAUSED" : "GAME UNPAUSED");
        }
    }

    // ========================
    //   QUIT GAME
    // ========================
    void HandleQuit()
    {
        if (Input.GetKeyDown(quitKey))
        {
            Debug.Log("Quit pressed.");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // ========================
    //   LEVEL MANAGEMENT
    // ========================

    public void OnPlayerEnteredTrigger(LevelTrigger triggerObject)
    {
        if (triggerObject.gameObject == levelTriggers[currentLevel])
        {
            triggerObject.StartLevel();
            currentLevel++;

            if (currentLevel >= levelTriggers.Length)
                currentLevel = levelTriggers.Length - 1;

            UpdateLevelState();
        }
        else
        {
            Debug.Log("Entered wrong level trigger!");
        }
    }

    void UpdateLevelState()
    {
        for (int i = 0; i < levelTriggers.Length; i++)
            levelTriggers[i].SetActive(i == currentLevel);

        for (int i = 0; i < levelBeams.Length; i++)
            levelBeams[i].SetActive(i == currentLevel);

        if (levelText != null)
            levelText.text = "Level " + (currentLevel + 1);
    }
}
