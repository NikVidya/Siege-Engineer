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
	[Tooltip("Upgrade prefabs to create as available upgrades")]
	public BaseUpgrade[] upgradeTemplates;
    [Tooltip("The GameObject containing the UI to display when showing upgrades")]
    public GameObject UpgradeWindow;
    [Tooltip("The GameObject to put the list entries under")]
    public GameObject UpgradeListContent;

	[HideInInspector]
	[System.NonSerialized]
    public int currencyAmount = 0;

	BaseUpgrade[] upgrades;

    void Start()
    {
		upgrades = new BaseUpgrade[upgradeTemplates.Length];
		for (int i = 0; i < upgradeTemplates.Length; i++) {
			GameObject upgradeInstance = GameObject.Instantiate (upgradeTemplates [i].gameObject, transform);
			BaseUpgrade upgradeComponent = upgradeInstance.GetComponent<BaseUpgrade> ();
			if (upgradeComponent != null) {
				upgrades [i] = upgradeComponent;
			} else {
				Debug.LogWarningFormat ("UpgradeManager - Invalid upgrade template: {0}. Does not have BaseUpgrade script attached", upgradeInstance.name);
			}
		}
		// Build the UI
		for (int i=0; i < upgrades.Length; i++){
			BaseUpgrade upgrade = upgrades [i];
			if (upgrade == null) {
				continue; // Skip this upgrade, as it can't be put in the list
			}
			GameObject entry = upgrade.GetListEntryObject ();
			if( entry == null) {
				continue; // Skip
			}

			entry.transform.SetParent(UpgradeListContent.transform);
			Button upgradeEntryButtonComponent = entry.GetComponent<Button> ();
			if (upgradeEntryButtonComponent != null) {
				int upgradeToPurchase = i; // Note, need to capture 'i' properly for the lambda below
                Button entityButton = upgradeEntryButtonComponent;
				upgradeEntryButtonComponent.onClick.AddListener (() => PurchaseUpgrade(upgradeToPurchase, entityButton));
			}
		}
    }

    void Update()
    {
		if(Input.GetButtonUp(Constants.InputNames.UICANCEL))
        {
            HideUpgradeWindow();
        }
    }

    void PurchaseUpgrade(int upgradeIndex, Button entryButton)
    {
		if (currencyAmount >= upgrades [upgradeIndex].UpgradeCost) {
			Debug.LogFormat ("Purchasing upgrade: {0}", upgrades [upgradeIndex].UpgradeName);
			upgrades [upgradeIndex].ApplyUpgrade (GameManager.Instance.GetPlayerObject());
			ChangeCurrencyAmount ((int) -(upgrades [upgradeIndex].UpgradeCost));
            entryButton.interactable = false;
        }
        else{
			Debug.LogFormat("Not enough currency for upgrade: {0}", upgrades[upgradeIndex].UpgradeName);
		}
    }

    public void ShowUpgradeWindow()
    {
        UpgradeWindow.SetActive(true);

		if (upgrades.Length > 0) {
			upgrades [0].SetEntrySelected ();
		}

        // Set the enabled state of the upgrade
        foreach(BaseUpgrade upgrade in upgrades)
        {
            GameObject entry = upgrade.GetListEntryObject();
            Button entryButton = entry.GetComponent<Button>();
            if (entryButton != entry)
            {
                entryButton.interactable = !upgrade.IsApplied;
            }
        }

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
