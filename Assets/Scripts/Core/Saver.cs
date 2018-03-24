using UnityEngine;
using System.Collections;

public class Saver : Singleton<Saver> {
    void Start() {
        if (!PlayerPrefs.HasKey(Constants.PlayerPrefsKeys.PLAYER_CURRENCY)){
            PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.PLAYER_CURRENCY, 0);
        }
        if (!PlayerPrefs.HasKey(Constants.PlayerPrefsKeys.PLAYER_SCORE)){
            PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.PLAYER_SCORE, 0);
        }
    }

    public void SavePlayerCurrency(int amount){
        PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.PLAYER_CURRENCY, amount);
        PlayerPrefs.Save();
    }
    public void AddToSavedPlayerCurrency(int amount) {
        int newAmount = PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.PLAYER_CURRENCY) + amount;
        PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.PLAYER_CURRENCY, newAmount);
        PlayerPrefs.Save();
    }
    public void SavePlayerScore(int amount){
        PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.PLAYER_SCORE, amount);
        PlayerPrefs.Save();
    }
    public void AddToSavedPlayerScore(int amount) {
        int newAmount = PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.PLAYER_SCORE) + amount;
        PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.PLAYER_SCORE, newAmount);
        PlayerPrefs.Save();
    }

}