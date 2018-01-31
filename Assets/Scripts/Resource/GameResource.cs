using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResource : MonoBehaviour, IHoldable
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

    private InteractionState _interactState = InteractionState.Ready;
    private HoldState _holdState = HoldState.Dropped;
    private Transform oldParent;

    private void Start()
    {
        // Register this resource with the game manager
        GameManager.Instance.RegisterInteractable(this, InteractionPriority.RESOURCE);
    }

    private void OnDestroy()
    {
        if (!GameManager.IsApplicationQuitting)
        {
            GameManager.Instance.DeRegisterInteractable(this, InteractionPriority.RESOURCE);
        }
    }

    public void OnInteract(CharacterInteraction instigator, CharacterInteraction.KeyState state)
    {
        if (state == CharacterInteraction.KeyState.Pressed && _holdState == HoldState.Dropped)
        {
            Pickup();
            instigator.InventoryComponent.AddHoldable(this);
        }
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
