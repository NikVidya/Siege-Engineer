using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeSequence : MonoBehaviour {

    [Header("Display Parameters")]
    [Tooltip("Font to display dialog in")]
    public Font dialogFont;
    [Tooltip("The animation controller for the talker")]
    public RuntimeAnimatorController talkerAnimatorController;
    
    [Header("Dialog Parameters")]
    [SerializeField]
    [TextArea(1,10)]
    [Tooltip("The text to display in the dialog box")]
    public string dialogText;
    [Tooltip("The speed to print out the characters. (characters per second)")]
    public float scrollSpeed = 25.0f;

    private BaseNarrativeAction[] actions;

    private void Start()
    {
        // Get all actions on this sequence
        actions = GetComponents<BaseNarrativeAction>();
    }

    public void StartSequence()
    {

    }

    public void AccelerateSequence()
    {

    }

    public void TerminateSequence()
    {

    }
}
