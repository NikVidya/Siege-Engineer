using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DialogUIController : MonoBehaviour {

    [Header("UI References")]
    public Animator CharacterPortrait;
    public Text DialogTextArea;

    Animator UIAnimator;

	// Use this for initialization
	void Start () {
        UIAnimator = GetComponent<Animator>();
        if(UIAnimator == null)
        {
            Debug.LogErrorFormat("DialogUIController '{0}' could not get a reference to it's animator!", gameObject.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
