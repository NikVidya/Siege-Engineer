﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Animator))]
public class DialogUIController : MonoBehaviour {

    public delegate void AnimationFinishedDelegate ();

    [Header ("UI References")]
    public Animator CharacterPortrait;
    public Text DialogTextArea;

    Animator UIAnimator;
    const string ANIMATOR_SHOW_DIALOG_TAG = "showDialog";
    const string ANIMATOR_EMOTION_STATE_TAG = "emotionState";

    AnimationFinishedDelegate showCallback = null;
    AnimationFinishedDelegate hideCallback = null;

    void Awake () {
        UIAnimator = GetComponent<Animator> ();
        if (UIAnimator == null) {
            Debug.LogErrorFormat ("DialogUIController '{0}' could not get a reference to it's animator!", gameObject.name);
        }
    }

    public void Initialize (NarrativeSequence sequence) {
        DialogTextArea.font = sequence.dialogFont;
        DialogTextArea.color = new Color(0,0,0);
        CharacterPortrait.runtimeAnimatorController = sequence.talkerAnimatorController;
    }

    public void PlayPortraitEmotionAnim (NarrativeSequencer.CharacterEmotion emotion) {
        CharacterPortrait.SetInteger (ANIMATOR_EMOTION_STATE_TAG, (int) emotion);
    }

    public void ShowDialog (AnimationFinishedDelegate callback) {
        OnFinishedHideDialog (); // We've finished hiding, since we're about to show it
        gameObject.SetActive (true); // Activate self before animation so it can be seen
        showCallback = callback;
        UIAnimator.SetBool (ANIMATOR_SHOW_DIALOG_TAG, true);
    }

    public void OnFinishedShowDialog () {
        if (showCallback != null) {
            showCallback ();
        }
        showCallback = null;
    }

    public void HideDialog (AnimationFinishedDelegate callback) {
        OnFinishedShowDialog (); // We've finished showing, since we're about to hide it
        hideCallback = callback;
        UIAnimator.SetBool (ANIMATOR_SHOW_DIALOG_TAG, false);
    }

    public void OnFinishedHideDialog () {
        gameObject.SetActive (false); // Deactivate self after animation so it will still play
        if (hideCallback != null) {
            hideCallback ();
        }
        hideCallback = null;
    }
}