using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameResource : MonoBehaviour, CharacterInventory.IHoldable {
    
    [HideInInspector]
    public enum ResourceState
    {
        Ready,
        InUse,
        Destroyed,
        Consumed
    }
    public ResourceState State { get; set; }

    CharacterInventory.HoldState CharacterInventory.IHoldable.State
    {
        get
        {
            return _holdState;
        }
    }

    GameObject CharacterInventory.IHoldable.gameObject
    {
        get
        {
            return this.gameObject;
        }
    }

    private CharacterInventory.HoldState _holdState = CharacterInventory.HoldState.Ready;
    private Transform oldParent;

    private void Start()
    {
        // Register this resource with the game manager
        GameManager.Instance.holdables.Add(this);
    }

    public void Pickup()
    {
        oldParent = transform.parent;
        _holdState = CharacterInventory.HoldState.Held;
    }

    public void Drop()
    {
        _holdState = CharacterInventory.HoldState.Dropped;
        transform.parent = oldParent;
    }

    public void SetBlocked(bool blocked)
    {
        if(blocked)
        {
            _holdState = CharacterInventory.HoldState.Blocked;
        }
        else
        {
            _holdState = CharacterInventory.HoldState.Ready;
        }
    }
}
