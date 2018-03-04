using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNav : MonoBehaviour {
	public void GameStart () {
		GameStateSwitcher.Instance.GameStart ();
	}
	public void Quit () {
		GameStateSwitcher.Instance.Quit ();
	}
}