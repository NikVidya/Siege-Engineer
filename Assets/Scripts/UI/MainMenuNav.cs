using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuNav : MonoBehaviour {
	public Button startGameButton;
	void Start () {

		EventSystem.current.SetSelectedGameObject (startGameButton.gameObject);

	}
	public void GameStart () {
		GameStateSwitcher.Instance.GameStart ();
	}
	public void LevelSelect () {
		GameStateSwitcher.Instance.LevelSelect ();
	}
	public void Quit () {
		GameStateSwitcher.Instance.Quit ();
	}
}