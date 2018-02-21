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
public class GameStateSwitcher : Singleton<GameStateSwitcher> {
    public CharacterMovement player;
    [HideInInspector]
    public string timeString;
    [Header ("Text UIs")]
    public Text timeUI;
    public Text pauseUI;
    [Header ("Time")]
    public int timeMinutes = 1;
    public float timeSeconds = 30; // Time in seconds after minute. for example, if minute = 1 and seconds = 30, timer is 1:30
    bool paused = false;
    float gameTimer;
    string pauseString = "PAUSED";
    void Start () {
        timeString = timeMinutes + ":" + Mathf.Floor (timeSeconds);
        gameTimer = (60 * timeMinutes) + timeSeconds;
        if (timeUI == null) {
            Debug.LogWarning ("Warning: Timer Text UI not passed to Game State Switcher, player won't know how much time remains");
        }
        if (pauseUI == null) {
            Debug.LogWarning ("No Pause Text UI was passed to Game State Switcher");
        } else {
            pauseUI.text = "";
        }
    }
    void Update () {
        if (Input.GetButtonDown (Constants.InputNames.PAUSE)) {
            if (!paused) {
                Pause ();
                Debug.Log ("PAUSED");
            } else {
                UnPause ();
            }
        }
        TimerLogic ();
    }

    void TimerLogic () {
        timeSeconds -= Time.deltaTime;
        gameTimer -= Time.deltaTime;

        if (timeSeconds <= 0) {
            timeSeconds = 60f;
            timeMinutes--;
        }

        string zero = "";
        if (timeSeconds < 10) { zero = "0"; } else { zero = ""; }
        timeString = timeMinutes + ":" + zero + Mathf.Floor (timeSeconds);
        if (timeUI != null) {
            timeUI.text = timeString;
        }
        if (gameTimer <= 0.01) {
            Win ();
        }
    }
    public void GameStart () {
        SceneManager.LoadScene ("Level01");
    }
    public void GameOver () {
        SceneManager.LoadScene ("GameOver");
    }
    public void Win () {
        SceneManager.LoadScene ("VictoryScreen");
    }
    public void EnterDialogue () {

    }
    public void ExitDialogue () {

    }

    public void Pause () {
        Time.timeScale = 0;
        if (player != null) {
            player.paused = true;
        } else {
            Debug.LogWarning ("Player Movement was not passed to GameStateSwitcher, player can still move when paused");
        }
        if (pauseUI != null) {
            pauseUI.text = pauseString;
        }
        paused = true;
    }
    public void UnPause () {
        Time.timeScale = 1;
        if (player != null) {
            player.paused = false;
        }
        if (pauseUI != null) {
            pauseUI.text = "";
        }
        paused = false;
    }
    public void Quit () {
        Application.Quit ();
    }
}