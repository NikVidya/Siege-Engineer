using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player touched me! Call the police!");
        }
        this.gameObject.SetActive(false);
		UpgradeManager.Instance.ChangeCurrencyAmount(1);
    }

}
