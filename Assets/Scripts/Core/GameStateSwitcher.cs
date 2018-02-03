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
}
