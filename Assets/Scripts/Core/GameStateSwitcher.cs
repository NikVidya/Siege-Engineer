using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
To handle changes in game state such as game over, new game, return to 
menu, exit, and entering/exiting dialogue

TODO:
* Expand functionality to save currency/stats between games
 */
public class GameStateSwitcher : Singleton<GameStateSwitcher>
{
    public CharacterMovement player;
    [HideInInspector]
    public string timeString;
    [Header("Text UIs")]
    public Text timeUI;
    public GameObject pauseUI;
    [Header("Time")]
    public bool countDown = true;
    bool paused = false;
    public float gameTimer = 151;
    // timeMinutes and timeSeconds used for the display time
    int timeMinutes = 1;
    float timeSeconds = 30; // Time in seconds after minute. for example, if minute = 1 and seconds = 30, timer is 1:30
    void Start()
    {
        gameTimer = (60 * timeMinutes) + timeSeconds;

        timeMinutes = (int)Mathf.Floor(gameTimer / 60);
        timeSeconds = gameTimer - (timeMinutes * 60);

        timeString = timeMinutes + ":" + Mathf.Floor(timeSeconds);

        if (timeUI == null)
        {
            Debug.LogWarning("Warning: Timer Text UI not passed to Game State Switcher, player won't know how much time remains");
        }
        if (pauseUI == null)
        {
            Debug.LogWarning("Pause UI not assigned to Game StateSwitcher");
        }
        else
        {
            pauseUI.gameObject.SetActive(false);
        }
    }
    void Update()
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

    void TimerLogic()
    {
        if (countDown)
        {
            timeSeconds -= Time.deltaTime;
            gameTimer -= Time.deltaTime;
            if (timeSeconds <= 0)
            {
                timeSeconds = 60f;
                timeMinutes--;
            }
        }
        else
        {
            timeSeconds += Time.deltaTime;
            gameTimer += Time.deltaTime;
            if (timeSeconds > 59)
            {
                timeSeconds = 0f;
                timeMinutes++;
            }
        }

        string zero = "";
        if (timeSeconds < 10) { zero = "0"; } else { zero = ""; }
        timeString = timeMinutes + ":" + zero + Mathf.Floor(timeSeconds);

        if (timeUI != null)
        {
            timeUI.text = timeString;
        }
        if (countDown && gameTimer <= 0.01)
        {
            Win();
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Level01");
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
            Debug.LogWarning("Player Movement script was not passed to GameStateSwitcher, player can still move when paused");
        }
        if (pauseUI != null)
        {
            pauseUI.gameObject.SetActive(true);
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
        if (pauseUI != null)
        {
            pauseUI.gameObject.SetActive(false);
        }
        paused = false;
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}