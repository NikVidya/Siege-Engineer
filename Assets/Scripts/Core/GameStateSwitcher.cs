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
public class GameStateSwitcher : Singleton<GameStateSwitcher> {
    public CharacterMovement player;
    [HideInInspector]
    public GameObject pauseUI;
    bool paused = false;
    public string nextLevel;
    // timeMinutes and timeSeconds used for the display time

    public string DebugStartupNarrativeScene;

    void Start () {
        if (pauseUI == null) {
            Debug.LogWarning ("Pause UI not assigned to Game StateSwitcher");
        } else {
            pauseUI.gameObject.SetActive (false);
        }
        if (!string.IsNullOrEmpty (DebugStartupNarrativeScene)) {
            CinematicManager.Instance.EnqueueCinematic (DebugStartupNarrativeScene);
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
    }
    public void Victory () {
        Debug.Log ("Victory!!!");
        // SceneManager.LoadScene (nextLevel);
    }
    public void GameStart () {
        SceneManager.LoadScene ("Level01");
    }
    public void GameOver () {
        SceneManager.LoadScene ("GameOver");
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
            Debug.LogWarning ("Player Movement script was not passed to GameStateSwitcher, player can still move when paused");
        }
        if (pauseUI != null) {
            pauseUI.gameObject.SetActive (true);
        }
        paused = true;
    }
    public void UnPause () {
        Time.timeScale = 1;
        if (player != null) {
            player.paused = false;
        }
        if (pauseUI != null) {
            pauseUI.gameObject.SetActive (false);
        }
        paused = false;
    }
    public void QuitToMenu () {
        SceneManager.LoadScene ("mainmenu");
    }
    public void Quit () {
        Application.Quit ();
    }
}