using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelValues : MonoBehaviour {
    //public
    public Text currencyUI;
    public Text currencyToAddUI;
    public Text scoreUI;
    [Tooltip ("These will have a short animation")]
    public GameObject[] CurrencyToAddAnim;
    //private
    private int playerCurrency;
    private int score;
    private int currencyDisplayNum;
    private int currencyToAddDisplayNum;
    private int scoreDisplayNum;

    void Start () {
        playerCurrency = PlayerPrefs.GetInt (Constants.PlayerPrefsKeys.PLAYER_CURRENCY);
        score = 100 * playerCurrency;
        Saver.Instance.SavePlayerScore (score);

        currencyDisplayNum = 50;
        currencyToAddDisplayNum = currencyDisplayNum;
        scoreDisplayNum = score;
        currencyUI.text = currencyDisplayNum.ToString ();
        currencyToAddUI.text = currencyToAddDisplayNum.ToString ();
        scoreUI.text = scoreDisplayNum.ToString ();
        foreach (GameObject g in CurrencyToAddAnim) {
            Vector3 newScale = g.transform.localScale;
            newScale.y = 0;
            g.transform.localScale = newScale;
        }
        InvokeRepeating ("AddCurrencyAnim", 2, 0.05f);
        InvokeRepeating ("UIMath", 4, 0.05f);
        Invoke ("Continue", 10);
    }

    void Update () { }

    private void UIMath () {
        if (currencyToAddDisplayNum > 0) {
            currencyToAddDisplayNum--;
            scoreDisplayNum += 100;
            currencyToAddUI.text = currencyToAddDisplayNum.ToString ();
            scoreUI.text = scoreDisplayNum.ToString ();
        } else {
            CancelInvoke ("UIMath");
        }
    }

    private void AddCurrencyAnim () {
        foreach (GameObject g in CurrencyToAddAnim) {
            if (g.transform.localScale.y < 1) {
                Vector3 newScale = g.transform.localScale;
                newScale.y += 0.1f;
                g.transform.localScale = newScale;
            } else {
                CancelInvoke("AddCurrencyAnim");
            }
        }
    }

    void Continue () {
        SceneManager.LoadScene ("levelmenu");
    }
}