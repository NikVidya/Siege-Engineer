using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* TODO
	1. Have the Damageable's health correspond to the healthbar graphic
		- Alternatively, ditch the healthbar and use Damageable damage sprites
	2. Placeholder sprites
 */
public class Damageable : MonoBehaviour, IInteractable, IBombable
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

    public bool ShouldEndGame = false;

    [HideInInspector]
    public bool repairState = false;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public float healthPercent = 100.0f;
    [HideInInspector]
    public float healthPercentChange;
    public UnityEvent healthChange;
	[HideInInspector]
    [System.NonSerialized]
	public float damageDeBuff = 0f;

    InteractionState _interactState = InteractionState.Ready;
    public InteractionState InteractState {
        get {
            return _interactState;
        }
        set {
            _interactState = value;
        }
	}

    public InteractableType InteractableType
    {
        get
        {
            return InteractableType.REPAIR;
        }
        set { }
    }

	float timeSpentRepairing = 0;

    //private
    void Start()
    {
        // Register this game object so it can be interacted with.
        GameManager.Instance.RegisterInteractable(this);

        health = maxHealth;
        InvokeRepeating("PeriodicHealthChange", gracePeriod, healthDecreaseRate);

        healthChange.Invoke();
    }

    void Update()
    {

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health > 0)
        {
            healthPercent = (maxHealth / health) * 100;
        }
        else
        {
            healthPercent = 0;
        }

		if (timeSpentRepairing > healthRepairTime) {
			repairState = false;
		} else if (repairState) {
			timeSpentRepairing += Time.deltaTime;
		}
    }


    void PeriodicHealthChange()
	{
        if(repairState)
        {
            ChangeHealth(healthRepairAmount);
        }
        else if (periodicDamage && !repairState)
        {
            int calcChangeAmount = Mathf.RoundToInt(healthDecreaseAmount * (1 - damageDeBuff));
            ChangeHealth(calcChangeAmount);
        }
    }

    /* 
	Change the health of the Damageable
	*/
    public void ChangeHealth(int changeAmount)
    {
        health += changeAmount;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0)
        {
            Die();
        }

        if (healthChange == null) { 
			healthChange = new UnityEvent(); 
		}
        healthPercent = (float)health / (float)maxHealth;
        // Debug.Log("current health is " + health);
        // Debug.Log("Damage debuff is: " + damageDeBuff);
        healthChange.Invoke();
    }

    void Die()
    {
        if (ShouldEndGame)
        {
            GameStateSwitcher.Instance.GameOver();
        }
    }

	public void OnBombed(Bombardment instigator){
		Debug.LogFormat ("Bombed {0} for {1} damage", gameObject.name, instigator.damage);
		this.health -= instigator.damage;
	}

	public float GetDistanceToTransform(Transform t)
	{
		return Vector3.Distance (t.position, transform.position);
	}

    public void OnInteract(CharacterInteraction instigator)
    {
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
            Debug.Log("Start Repair");
			timeSpentRepairing = 0;
        }
    }

    public void OnFocus(CharacterInteraction focuser)
    {
    }

    public void OnDefocus(CharacterInteraction focuser)
    {
    }
}