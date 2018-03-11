using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeSequencer : MonoBehaviour {
    [Header("UI References")]
    [Tooltip("Controller for left side character dialog")]
    public DialogUIController leftController;
    [Tooltip("Controller for right side character dialog")]
    public DialogUIController rightController;

    [Header("Sequence Queue")]
    [Tooltip("The NarrativeSequences to play, in order of playback")]
    public NarrativeSequence[] sequenceQueue;
}
