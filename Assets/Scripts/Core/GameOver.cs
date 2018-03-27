using UnityEngine;

public class GameOver : MonoBehaviour {
    void Start() {
        Invoke("BackToMenu", 10);
    }
    void BackToMenu(){
        GameStateSwitcher.Instance.QuitToMenu();
    }
}