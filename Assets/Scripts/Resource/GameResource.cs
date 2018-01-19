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

    public void Pickup(GameObject container)
    {
        oldParent = transform.parent;
        _holdState = CharacterInventory.HoldState.Held;
        if (container)
        {
            // TODO: Implement a handler for something being added to the inventory. i.e. play an animation, or handle placement
            transform.position = container.transform.position;
            transform.parent = container.transform;
        }
        else
        {
            // TODO: Hide the object since we aren't attaching it to a world container
            throw new System.NotImplementedException();
        }
    }

    public void Drop(Vector3 dropOrigin)
    {
        _holdState = CharacterInventory.HoldState.Dropped;
        transform.parent = oldParent;
        transform.position = dropOrigin;
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
