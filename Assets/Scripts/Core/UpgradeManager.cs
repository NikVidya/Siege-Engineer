using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public Text currencyUI;

	public BaseUpgrade[] upgrades;

    [HideInInspector]
    public int currencyAmount = 0;
    void Start()
    {
		
    }

    void Update()
    {
		
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
