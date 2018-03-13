using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNarrativeAction : MonoBehaviour {

    public abstract void OnSequenceStart();
    public abstract void OnSequenceEnd();
}
