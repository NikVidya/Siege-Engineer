using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Resource : MonoBehaviour {

    public GameObject prompt;

    private void Start()
    {
        prompt.SetActive(false);
    }

    public void ShowPrompt(bool isShown)
    {
        prompt.SetActive(isShown);
    }
}
