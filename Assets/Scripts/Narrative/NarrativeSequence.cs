using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeSequence : MonoBehaviour {

    [Header("Display Parameters")]
    [Tooltip("Font to display dialog in")]
    public Font dialogFont;
    [Tooltip("The animation controller for the talker")]
    public RuntimeAnimatorController talkerAnimatorController;
    [Tooltip("The type of dialog to use for this sequence")]
    public NarrativeSequencer.DialogStructure dialogType;
    [Tooltip("The speed to print out the characters. (characters per second)")]
    public float scrollSpeed = 25.0f;
    [Tooltip("Should this sequence auto advance to the next one")]
    public bool autoAdvance = false;
    [Tooltip("Delay before advancing if auto advance is enabled")]
    public float advanceDelay = 3.0f;

    [Header("Dialog Parameters")]
    [SerializeField]
    [TextArea(1,10)]
    [Tooltip("The text to display in the dialog box")]
    public string dialogText;
    [Tooltip("The emotional state of the character")]
    public NarrativeSequencer.CharacterEmotion emotionState;

    private BaseNarrativeAction[] actions;

    private void Start()
    {
        // Get all actions on this sequence
        actions = GetComponents<BaseNarrativeAction>();
    }

    public bool NeedsNewDialog(NarrativeSequence prev)
    {
        // Different character?
        if(prev.talkerAnimatorController != talkerAnimatorController)
        {
            return true;
        }

        // Different type?
        if (prev.dialogType != dialogType)
        {
            return true;
        }

        // Different font?
        if(prev.dialogFont != dialogFont)
        {
            return true;
        }

        return false;
    }

    public void OnSequenceStart()
    {
        foreach(BaseNarrativeAction action in actions)
        {
            action.OnSequenceStart();
        }
    }

    public void OnSequenceEnd()
    {
        foreach (BaseNarrativeAction action in actions)
        {
            action.OnSequenceEnd();
        }
    }
}
