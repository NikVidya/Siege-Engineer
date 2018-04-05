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
    public GameObject pauseUI;
    bool paused = false;

    public string IntroCinematicScene;
    public string MidCinematicScene;
    public string VictoryScene;

    void Start () {
        if (pauseUI == null) {
            Debug.LogWarning ("Pause UI not assigned to Game State Switcher");
        } else {
            pauseUI.gameObject.SetActive (false);
        }
        if (!string.IsNullOrEmpty (IntroCinematicScene)) {
            CinematicManager.Instance.EnqueueCinematic (IntroCinematicScene);
        }
    }
    void Update () {
        if (Input.GetButtonDown (Constants.InputNames.AUTOVICTORY)) {
            Debug.Log ("Oi! Don't press that, that's cheating!");
        }
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
        Saver.Instance.SavePlayerCurrency (UpgradeManager.Instance.currencyAmount);
        SceneManager.LoadScene (VictoryScene);
    }
    public void VictoryScreen () {
        SceneManager.LoadScene ("VictoryScreen");
    }
    public void GameStart () {
        SceneManager.LoadScene ("Level01");
    }
    public void TriggerMidCinematic () {
        if (!string.IsNullOrEmpty (MidCinematicScene)) {
            CinematicManager.Instance.EnqueueCinematic (MidCinematicScene);
        }
    }
    public void LoadLevel2 () {
        SceneManager.LoadScene ("Level02");
    }

    public void LoadLevel3 () {
        SceneManager.LoadScene ("Level03");
    }

    public void LoadLevel4 () {
        SceneManager.LoadScene ("Level04");
    }

    public void LoadLevel5 () {
        SceneManager.LoadScene ("Level05");
    }

    public void LoadTutorial () {
        SceneManager.LoadScene ("Tutorial");
    }

    public void GameOver () {
        SceneManager.LoadScene ("GameOver");
    }

    public void LevelSelect () {
        SceneManager.LoadScene ("levelmenu");
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