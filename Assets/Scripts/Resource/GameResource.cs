using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResource : MonoBehaviour, IHoldable, IBombable
{

    [Header("Resource Type")]
    public Constants.Resource.ResourceType type = Constants.Resource.ResourceType.WOOD;

    [HideInInspector]
    public enum ResourceState
    {
        Ready,
        InUse,
        Destroyed,
        Consumed
    }
    public ResourceState State { get; set; }

    InteractionState IInteractable.InteractState
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

    HoldState IHoldable.HeldState
    {
        get
        {
            return _holdState;
        }
        set
        {
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

    InteractableType IInteractable.InteractableType
    {
        get
        {
            return InteractableType.PICKUP;
        }

        set
        {
        }
    }

    private InteractionState _interactState = InteractionState.Ready;
    private HoldState _holdState = HoldState.Dropped;
    private Transform oldParent;

    private void Start()
    {
        // Register this resource with the game manager
        GameManager.Instance.RegisterInteractable(this);
		GameManager.Instance.RegisterBombable (this);
    }

    private void OnDestroy()
    {
        if (!GameManager.IsApplicationQuitting)
        {
            GameManager.Instance.DeRegisterInteractable(this);
        }
    }

    public void OnInteract(CharacterInteraction instigator)
    {
        if (_holdState == HoldState.Dropped)
        {
            Pickup();
            instigator.InventoryComponent.AddHoldable(this);
        }
    }

	public void OnBombed(Bombardment instigator)
	{
		if (_holdState == HoldState.Held) {
			Debug.LogFormat ("Bomb dropped {0} from the player's hand", gameObject.name);
			// Drop if held
			GameManager.Instance.interactionComponent.InventoryComponent.DropHeld (this);
		} else if (_holdState == HoldState.Dropped) {
			Debug.LogFormat ("Bomb destroyed {0} from the ground", gameObject.name);
			// Destroy if on the ground
			gameObject.SetActive (false);
		}
	}

	public float GetDistanceToTransform(Transform t)
	{
		return Vector3.Distance (t.position, transform.position);
	}

    public void Pickup()
    {
        oldParent = transform.parent;
        _holdState = HoldState.Held;
        _interactState = InteractionState.Activated;
    }

    public void Drop()
    {
        _holdState = HoldState.Dropped;
        _interactState = InteractionState.Ready;
        transform.parent = oldParent;
    }
}
