using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelValues : MonoBehaviour {
    //public
    public Text currencyUI;
    public Text currencyToAddUI;
    public Text scoreUI;
    public Text bonusToAddUI;
    [Header ("These will have a short animation")]
    public GameObject[] currencyToAddAnim;
    public GameObject[] bonusToAddAnim;
    //private
    private int playerCurrency;
    private int score;
    private int currencyDisplayNum;
    private int currencyToAddDisplayNum;
    private int scoreDisplayNum;
    private int bonus = 1000;

    void Start () {
        playerCurrency = PlayerPrefs.GetInt (Constants.PlayerPrefsKeys.PLAYER_CURRENCY);
        score = 100 * playerCurrency;
        score += bonus;
        Saver.Instance.SavePlayerScore (score);
        scoreDisplayNum = score;
        currencyDisplayNum = playerCurrency;
        currencyToAddDisplayNum = currencyDisplayNum;
        currencyUI.text = currencyDisplayNum.ToString ();
        currencyToAddUI.text = currencyToAddDisplayNum.ToString ();
        scoreUI.text = scoreDisplayNum.ToString ();
        foreach (GameObject g in currencyToAddAnim) {
            Vector3 newScale = g.transform.localScale;
            newScale.y = 0;
            g.transform.localScale = newScale;
        }
        bonusToAddUI.text = (bonus.ToString ());
        foreach (GameObject g in bonusToAddAnim) {
            Vector3 newScale = g.transform.localScale;
            newScale.y = 0;
            g.transform.localScale = newScale;
        }
        InvokeRepeating ("AddCurrencyAnim", 2, 0.05f);
        InvokeRepeating ("AddCurrencyMath", 4, 0.05f);
        Invoke ("Continue", 15);
    }

    void Update () { }

    private void AddCurrencyMath () {
        if (currencyToAddDisplayNum > 0) {
            currencyToAddDisplayNum--;
            scoreDisplayNum += 100;
            currencyToAddUI.text = currencyToAddDisplayNum.ToString ();
            scoreUI.text = scoreDisplayNum.ToString ();
        } else {
            foreach (GameObject g in currencyToAddAnim) {
                Vector3 newScale = g.transform.localScale;
                newScale.y = 0;
                g.transform.localScale = newScale;
            }
            InvokeRepeating ("AddBonusAnim", 1, 0.05f);
            InvokeRepeating ("AddBonusMath", 3, 0.03f);
            CancelInvoke ("UIMath");
        }
    }

    private void AddCurrencyAnim () {
        foreach (GameObject g in currencyToAddAnim) {
            if (g.transform.localScale.y < 1) {
                Vector3 newScale = g.transform.localScale;
                newScale.y += 0.1f;
                g.transform.localScale = newScale;
            } else {
                CancelInvoke ("AddCurrencyAnim");
            }
        }
    }

    private void AddBonusMath () {
        if (bonus > 0) {
            bonus--;
            scoreDisplayNum++;
            bonusToAddUI.text = bonus.ToString ();
            scoreUI.text = scoreDisplayNum.ToString ();
        } else {
            CancelInvoke ("UIMath");
        }
    }
    private void AddBonusAnim () {
        foreach (GameObject g in bonusToAddAnim) {
            if (g.transform.localScale.y < 1) {
                Vector3 newScale = g.transform.localScale;
                newScale.y += 0.1f;
                g.transform.localScale = newScale;
            } else {
                CancelInvoke ("AddBonusAnim");
            }
        }
    }
    void Continue () {
        SceneManager.LoadScene ("levelmenu");
    }
}