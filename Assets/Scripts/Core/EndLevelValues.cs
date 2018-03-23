using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelValues : MonoBehaviour
{
    //public
    public Text currencyUI;
    public Text currencyToAddUI;
    public Text scoreUI;
    //private
    private int playerCurrency;
    private int score;
    private int currencyDisplayNum;
    private int currencyToAddDisplayNum;
    private int scoreDisplayNum;

    void Start()
    {
        playerCurrency = PlayerPrefs.GetInt(Constants.PlayerPrefsTags.PLAYER_CURRENCY);
        score = 100 * playerCurrency;
        PlayerPrefs.SetInt(Constants.PlayerPrefsTags.PLAYER_SCORE, score);
        PlayerPrefs.Save();

        currencyDisplayNum = 50;
        currencyToAddDisplayNum = currencyDisplayNum;
        scoreDisplayNum = score;
        currencyUI.text = currencyDisplayNum.ToString();
        currencyToAddUI.text = currencyToAddDisplayNum.ToString();
        scoreUI.text = scoreDisplayNum.ToString();

        InvokeRepeating("UIMath", 3, 0.05f);
    }

    void Update()
    {
    }

    private void UIMath()
    {
        if (currencyToAddDisplayNum > 0)
        {
            currencyToAddDisplayNum--;
            scoreDisplayNum += 100;
            currencyToAddUI.text = currencyToAddDisplayNum.ToString();
            scoreUI.text = scoreDisplayNum.ToString();
        }
        else
        {
            CancelInvoke("UIMath");
        }
    }
}