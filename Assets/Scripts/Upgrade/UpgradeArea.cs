using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : MonoBehaviour, IInteractable {
    
    InteractionState _interactState = InteractionState.Ready;
    public InteractionState InteractState
    {
        get
        {
            return _interactState;
        }
        set
        {
            _interactState = value;
        }
    }

    GameObject IInteractable.gameObject
    {
        get
        {
            return this.gameObject;
        }
    }
    // Use this for initialization
    void Start () {
        GameManager.Instance.RegisterInteractable(this, InteractionPriority.UPGRADE_AREA);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnInteract(CharacterInteraction instigator, CharacterInteraction.KeyState state)
    {
        UpgradeManager.Instance.ShowUpgradeWindow();
    }

    public void OnFocus(CharacterInteraction focuser)
    {

    }

    public void OnDefocus(CharacterInteraction focuser)
    {
        UpgradeManager.Instance.HideUpgradeWindow();
        Debug.Log("Defocused Upgrade");
    }
}
