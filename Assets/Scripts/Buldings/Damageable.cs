using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO
	1. Have the Damageable's health correspond to the healthbar graphic
		- Alternatively, ditch the healthbar and use Damageable damage sprites
	2. Placeholder sprites
 */
public class Damageable : MonoBehaviour, IInteractable
{
    [Header("Health Parameters")]
    [Tooltip("The maximum health of the building")]
    public int maxHealth = 100;

    [Header("Damage Parameters")]
    [Tooltip("Whether this object takes periodic damage")]
    public bool periodicDamage = true;
    [Tooltip("The rate at which the Damageable heals or damages periodically")]
    public int healthDecreaseRate = 1;
    [Tooltip("Delay before incremental health decrease begins")]
    public int gracePeriod = 5;
    [Tooltip("The amount of damage the Damageable receives periodically")]
    public int healthDecreaseAmount = -3;

    [Header("Repair Parameters")]
    [Tooltip("The amount that the Damageable heals periodically")]
    public int healthRepairAmount = 10;
	[Tooltip("The amount of time invoking repair will repair for")]
	public float healthRepairTime = 1.0f;
    [Tooltip("What resources are required for repair")]
    public Constants.Resource.ResourceType[] repairCost;

    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool repairState = false;

    InteractionState _interactState = InteractionState.Ready;
    public InteractionState InteractState {
        get {
            return _interactState;
        }
        set {
            _interactState = value;
        }
	}

	float timeSpentRepairing = 0;

    //private
    void Start()
    {
        // Register this game object so it can be interacted with.
        GameManager.Instance.RegisterInteractable(this, InteractionPriority.STRUCTURE);


        health = maxHealth;
        InvokeRepeating("PeriodicHealthChange", gracePeriod, healthDecreaseRate);
    }

    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            periodicDamage = repairState = false;
            Die();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
		}

		if (timeSpentRepairing > healthRepairTime) {
			repairState = false;
		} else if (repairState) {
			timeSpentRepairing += Time.deltaTime;
		}
    }

    void PeriodicHealthChange()
	{
		if (repairState) {
			ChangeHealth (healthRepairAmount);
		}
        else if (periodicDamage)
        {
            ChangeHealth(healthDecreaseAmount);
        }
    }

    /* 
	Change the health of the Damageable
	*/
    public void ChangeHealth(int changeAmount)
    {
        int healthPrevious = health;
        if (changeAmount > 0 && health > maxHealth - changeAmount)
        {
            health += maxHealth - health;
        }
        else
        {
            health += changeAmount;
        }
        //Debug.Log("Health changed " + (health - healthPrevious));
        //Debug.Log("Current Damageable health is " + health);
    }
    void Die()
    {
		GameStateSwitcher.Instance.GameOver ();
    }

    public void OnInteract(CharacterInteraction instigator, CharacterInteraction.KeyState state)
    {
        if (state != CharacterInteraction.KeyState.Held)
        {
            return; // Early return because I dislike nesting
        }

        Dictionary<Constants.Resource.ResourceType, GameResource> availableResources = new Dictionary<Constants.Resource.ResourceType, GameResource>();

        // Note: If it where possible for multiple instances of resources to be on the map at once, the order 
        //  that the next two sections are in will determine if resources are used out of the player's inventory
        //  first, or off the ground first.

        // Find nearby resources
        List<GameResource> nearbyResources = GameManager.Instance.GetResourcesInRange(instigator.transform, instigator.interactDistance);
        foreach(GameResource gr in nearbyResources)
        {
            availableResources[gr.type] = gr; // This is set to be used
        }

        // Find resources in player inventory
        foreach(GameResource gr in instigator.InventoryComponent.heldInventory)
        {
            availableResources[gr.type] = gr; // This is set to be used
        }

        bool canRepair = true;
        foreach (Constants.Resource.ResourceType cost in repairCost)
        {
            canRepair = availableResources.ContainsKey(cost);
            if (!canRepair)
            {   // No sense continuing, we can't repair
                return;
            }
        }

        if (canRepair)
        {
            foreach (Constants.Resource.ResourceType cost in repairCost)
            {
                GameManager.Instance.ConsumeResource(availableResources[cost]);
			}
			repairState = true;
			timeSpentRepairing = 0;
        }
    }}