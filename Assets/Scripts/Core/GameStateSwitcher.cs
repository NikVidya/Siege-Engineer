using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
To handle changes in game state such as game over, new game, return to 
menu, exit, and entering/exiting dialogue

TODO:
* Make timer logic more multipurpose. Right now only works for a time countdown,
needs to work for a timer for other game modes.
* 
 */
public class GameStateSwitcher : Singleton<GameStateSwitcher>
{
    public CharacterMovement player;
    [HideInInspector]
    public string timeString;
    [Header("Text UIs")]
    public Text timeUI;
    public Text pauseUI;
    bool paused = false;
    int timeMinutes = 1;
    float timeSeconds = 30; // Time in seconds after minute. for example, if minute = 1 and seconds = 30, timer is 1:30
    float gameTimer;
    string pauseString = "PAUSED";
    void Start()
    {
        timeString = timeMinutes + ":" + Mathf.Floor(timeSeconds);
        gameTimer = (60 * timeMinutes) + timeSeconds;
        pauseUI.text = "";
    }
    void Update()
    {
        if (gameTimer <= 0.01)
        {
            Win();
        }
        else
        {
            if (Input.GetButtonDown(Constants.InputNames.PAUSE))
            {
                if (!paused)
                {
                    Pause();
                    Debug.Log("PAUSED");
                }
                else
                {
                    UnPause();
                }
            }
            TimerLogic();
        }
    }

    void TimerLogic()
    {
        timeSeconds -= Time.deltaTime;
        gameTimer -= Time.deltaTime;
        if (timeSeconds <= 0)
        {
            timeSeconds = 60f;
            timeMinutes--;
        }
        string zero = "";
        if (timeSeconds < 10) { zero = "0"; } else { zero = ""; }
        timeString = timeMinutes + ":" + zero + Mathf.Floor(timeSeconds);
        timeUI.text = timeString;
    }
    public void GameStart()
    {
        SceneManager.LoadScene("DebugLand");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void Win()
    {
        SceneManager.LoadScene("VictoryScreen");
    }
    public void EnterDialogue()
    {

    }
    public void ExitDialogue()
    {

    }

    public void Pause()
    {
        Time.timeScale = 0;
        if (player != null)
        {
            player.paused = true;
        }
        else
        {
            Debug.LogWarning("No player object for game state switcher to pause");
        }
        if (pauseUI != null)
        {
            pauseUI.text = pauseString;
        }
        paused = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        if (player != null)
        {
            player.paused = false;
        }
        else
        {
            Debug.LogWarning("No player object for game state switcher to unpause");
        }
        if (pauseUI != null)
        {
            pauseUI.text = "";
        }
        paused = false;
    }
}
