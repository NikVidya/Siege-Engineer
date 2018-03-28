using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNarrativeAction : MonoBehaviour {

    public abstract void OnSequenceStart();
    public abstract void OnSequenceEnd();
    public abstract void OnUpdate();

    void Update()
    {
        OnUpdate();
    }
}
