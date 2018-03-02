using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public enum UpgradeType
    {
        BootsOfPower = 0,
        FizzyLiftingDrink = 1
    }

    [Header("UI References")]
    public Text currencyUI;

    [Tooltip("The Object containing the UI to display when showing upgrades")]
    public GameObject UpgradeWindow;
    [Tooltip("The first entry in the list, to be focused when UI is displayed")]
    public GameObject FirstListEntry;

    [HideInInspector]
    public int currencyAmount = 0;
    void Start()
    {

    }

    void Update()
    {
		if(Input.GetButtonUp(Constants.InputNames.UICANCEL))
        {
            HideUpgradeWindow();
        }
    }

    public void PurchaseUpgrade(int type)
    {
        Debug.LogFormat("Purchasing upgrade: {0}", type);
    }

    public void ShowUpgradeWindow()
    {
        UpgradeWindow.SetActive(true);
        FirstListEntry.GetComponent<Button>().Select();
        GameManager.Instance.DisableAvatar();
    }
    public void HideUpgradeWindow()
    {
        UpgradeWindow.SetActive(false);
        GameManager.Instance.EnableAvatar();
    }

    public void ChangeCurrencyAmount(int n)
    {
        currencyAmount += n;
        if (currencyUI != null)
        {
            currencyUI.text = currencyAmount.ToString();
        }
    }
}
