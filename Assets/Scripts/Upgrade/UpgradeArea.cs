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

	public InteractableType InteractableType {
		get {
			return InteractableType.PICKUP;
		}
		set {
		}
	}

    // Use this for initialization
    void Start () {
		GameManager.Instance.RegisterInteractable(this);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnInteract(CharacterInteraction instigator)
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
