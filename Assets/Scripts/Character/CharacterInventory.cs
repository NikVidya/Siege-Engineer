using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour, IBombable
{
    [Header("Pickup Parameters")]
    [Tooltip("The in world container for the inventory. Leave as none to have no visible inventory")]
    public ResourceWorldContainer worldContainer;

    [Header("Visuals")]
    [Tooltip("The animator component which controls the sprite animation")]
    public Animator animator;

    private const string ANIMATOR_CARRYING_TAG = "isCarrying";

    public int NumHeldItems
    {
        get
        {
            return heldInventory.Count;
        }
    }

    public List<IHoldable> heldInventory = new List<IHoldable>();

    private void Start()
    {
		GameManager.Instance.RegisterBombable (this);
    }

    void Update()
    {
        /*if (Vector3.Distance(repairTarget.gameObject.transform.position, transform.position) < 3)
        {
            RepairGate();
        }*/

        if (heldInventory.Count <= 0)
        {
            animator.SetBool(ANIMATOR_CARRYING_TAG, false);
        }
    }

    public void AddHoldable(IHoldable holdable)
    {
        worldContainer.PlaceInContainer(holdable.gameObject);
        heldInventory.Add(holdable);
        animator.SetBool(ANIMATOR_CARRYING_TAG, true);

    }

    public void DropFirstHeld()
    {
        if (heldInventory.Count > 0)
        {
            worldContainer.RemoveFromContainer(heldInventory[0].gameObject, transform.position);
			heldInventory [0].Drop ();
            heldInventory.RemoveAt(0);
        }
    }

	public void DropHeld(IHoldable held)
	{
		worldContainer.RemoveFromContainer (held.gameObject, transform.position);
		held.Drop ();
		heldInventory.Remove (held);
	}

	public void OnBombed(Bombardment instigator)
	{
		if (NumHeldItems <= 0) {
			CharacterMovement movementComponent = GetComponent<CharacterMovement> ();
			if (movementComponent != null) {
				movementComponent.Stun ();
			}
		}
	}

	public float GetDistanceToTransform(Transform t)
	{
		return Vector3.Distance (transform.position, t.position);
	}
}
