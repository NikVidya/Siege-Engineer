using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
To handle changes in game state such as game over, new game, return to 
menu, exit, and entering/exiting dialogue
 */
public class GameStateSwitcher : Singleton<GameStateSwitcher>
{
    public Canvas pauseUI;
    public CharacterMovement player;
    private bool paused = false;

    void Start()
    {
        pauseUI.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown(Constants.InputNames.PAUSE))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("DebugLand");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void EnterDialogue()
    {

    }

    public void Pause()
    {
        Time.timeScale = 0;
        player.paused = true;
        paused = true;
        pauseUI.gameObject.SetActive(true);
        Debug.Log("Pause");
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        player.paused = false;
        paused = false;
        pauseUI.gameObject.SetActive(false);
        Debug.Log("UnPause");
    }
}
