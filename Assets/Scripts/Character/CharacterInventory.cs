using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{

    // Interface for objects which can be held by the player
    public enum HoldState
    {
        Ready,
        Held,
        Dropped,
        Blocked
    }
    public interface IHoldable
    {
        void Pickup();
        void Drop();
        void SetBlocked(bool blocked);
        HoldState State { get; }
        GameObject gameObject { get; }
    }

    [Header("Pickup Parameters")]
    [Tooltip("Maximum distance that a resource can be picked up")]
    public float pickupDistance = 1.0f;
    [Tooltip("The in world container for the inventory. Leave as none to have no visible inventory")]
    public ResourceWorldContainer worldContainer;

    [Tooltip("The GameObject or prefab to use as indication that an object can be picked up")]
    public GameObject interactionPrompt;

    public Damageable repairTarget;
    public int NumHeldItems
    {
        get
        {
            return heldInventory.Count;
        }
    }

    private List<IHoldable> heldInventory = new List<IHoldable>();

    private void Start()
    {
        if (!interactionPrompt)
        {
            Debug.LogError("Character Inventory does not have an interaction prompt!");
        }

        interactionPrompt.SetActive(false);
    }

    void Update()
    {
        IHoldable focused = FocusCloseHoldable();
        if (focused != null && Input.GetButtonUp(Constants.InputNames.INTERACT))
        {
            TakeHoldable(focused);
        }
        else if (Input.GetButtonUp(Constants.InputNames.INTERACT) && heldInventory.Count > 0)
        {   // Want to drop an item, and the button was released since the last time we dropped an item
            DropHoldable();
        }
        if (Vector3.Distance(repairTarget.gameObject.transform.position, transform.position) < 3)
        {
            RepairGate();
        }
    }

    void TakeHoldable(IHoldable holdable)
    {
        holdable.Pickup();
        worldContainer.PlaceInContainer(holdable.gameObject);
        heldInventory.Add(holdable);
    }

    void DropHoldable()
    {
        worldContainer.RemoveFromContainer(heldInventory[0].gameObject, transform.position);
        heldInventory[0].Drop();
        heldInventory.RemoveAt(0);
    }

    void ShowPrompt(GameObject target)
    {
        interactionPrompt.SetActive(true);
        interactionPrompt.transform.position = target.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        interactionPrompt.transform.parent = target.transform;
    }

    void HidePrompt()
    {
        interactionPrompt.SetActive(false);
    }

    IHoldable FocusCloseHoldable()
    {
        IHoldable closest = null;
        float closestDistance = float.MaxValue;

        foreach (IHoldable holdable in GameManager.Instance.holdables)
        {
            // Get distance to this holdable
            float dist = Vector3.Distance(holdable.gameObject.transform.position, transform.position);
            if (dist < closestDistance && dist <= pickupDistance && (holdable.State == HoldState.Ready || holdable.State == HoldState.Dropped))
            { // Close enough to pick up, and is the closest so far
                closest = holdable;
                closestDistance = dist;
            }
        }

        // Found closest if it exists
        if (closest != null)
        {   // Show the prompt
            ShowPrompt(closest.gameObject);
        }
        else
        {   // Hide the prompt
            HidePrompt();
        }

        return closest;
    }

    // Repair system
    void RepairGate()
    {
        if (Input.GetButton(Constants.InputNames.REPAIR))
        {
            repairTarget.repairState = true;
        }
        else
        {
            repairTarget.repairState = false;
        }
    }
}
