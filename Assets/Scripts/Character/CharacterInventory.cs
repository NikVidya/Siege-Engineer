using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [Header("Pickup Parameters")]
    [Tooltip("Maximum distance that a resource can be picked up")]
    public float pickupDistance = 1.0f;
    [Tooltip("The in world container for the inventory. Leave as none to have no visible inventory")]
    public ResourceWorldContainer worldContainer;

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
    }

    void Update()
    {
        /*if (Vector3.Distance(repairTarget.gameObject.transform.position, transform.position) < 3)
        {
            RepairGate();
        }*/
    }

    public void AddHoldable(IHoldable holdable)
    {
        Debug.LogFormat("Attaching object {0} to inventory", holdable.gameObject.name);
        worldContainer.PlaceInContainer(holdable.gameObject);
        heldInventory.Add(holdable);
    }

    public void DropFirstHeld()
    {
        if (heldInventory.Count > 0)
        {
            Debug.LogFormat("Detaching object {0} to inventory", heldInventory[0].gameObject.name);
            worldContainer.RemoveFromContainer(heldInventory[0].gameObject, transform.position);
			heldInventory [0].Drop ();
            heldInventory.RemoveAt(0);
        }
    }

    /*
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
    */
}
