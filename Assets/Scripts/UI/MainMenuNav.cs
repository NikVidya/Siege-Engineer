using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNav : MonoBehaviour {
	public void GameStart () {
		GameStateSwitcher.Instance.GameStart ();
	}
	public void LevelSelect() {
		GameStateSwitcher.Instance.LevelSelect();
	}
	public void Quit () {
		GameStateSwitcher.Instance.Quit ();
	}
}