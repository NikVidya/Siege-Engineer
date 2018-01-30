using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResource : MonoBehaviour, IHoldable {
    
    [HideInInspector]
    public enum ResourceState
    {
        Ready,
        InUse,
        Destroyed,
        Consumed
    }
    public ResourceState State { get; set; }

	InteractionState IInteractable.interactState {
		get {
			return _interactState;
		}
		set {
			_interactState = value;
		}
	}

    HoldState IHoldable.heldState
    {
        get
        {
            return _holdState;
        }
		set {
			_holdState = value;
		}
    }

    GameObject IInteractable.gameObject
    {
        get
        {
            return this.gameObject;
        }
    }

	private InteractionState _interactState = InteractionState.Ready;
    private HoldState _holdState = HoldState.Dropped;
    private Transform oldParent;

    private void Start()
    {
        // Register this resource with the game manager
        GameManager.Instance.holdables.Add(this);
    }

	private void OnDestroy()
	{
		if (!GameManager.IsApplicationQuitting) {
			GameManager.Instance.holdables.Remove (this);
		}
	}

	public void OnInteract (CharacterInteraction.KeyState state)
	{
		switch (state) {
		case CharacterInteraction.KeyState.Pressed:
			break;

		case CharacterInteraction.KeyState.Released:

			break;
		}
	}

	public void SetBlocked (bool blocked)
	{
		throw new System.NotImplementedException ();
	}

    public void Pickup()
    {
        oldParent = transform.parent;
        _holdState = HoldState.Held;
    }

    public void Drop()
    {
        _holdState = HoldState.Dropped;
        transform.parent = oldParent;
    }
}
