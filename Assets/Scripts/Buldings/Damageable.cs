using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* TODO
	1. Have the Damageable's health correspond to the healthbar graphic
		- Alternatively, ditch the healthbar and use Damageable damage sprites
	2. Placeholder sprites
 */
public class Damageable : MonoBehaviour
{
    //public
    [Tooltip("Whether this object takes periodic damage")]
    public bool periodicDamage = true;
    [Tooltip("The rate at which the Damageable heals or damages periodically")]
    public int healthDecreaseRate = 1;
    [Tooltip("Delay before incremental health decrease begins")]
    public int gracePeriod = 5;
    [Tooltip("The amount of damage the Damageable receives periodically")]
    public int healthDecreaseAmount = -3;
    [Tooltip("The amount that the Damageable heals periodically")]
    public int healthRepairAmount = 10;
    [Tooltip("The maximum health of the building")]
    public int maxHealth = 100;
    [HideInInspector]
    public bool repairState = false;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public float healthPercent;
    [HideInInspector]
    public float healthPercentChange;
    public UnityEvent healthChange;
    //private
    void Start()
    {
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
        healthPercent = (maxHealth / health) * 100;
    }

    void PeriodicHealthChange()
    {
        if (repairState)
        {
            ChangeHealth(healthRepairAmount);
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
        float healthPrevious = health;
        if (changeAmount > 0 && health > maxHealth - changeAmount)
        {
            health += maxHealth - health;
        }
        else
        {
            health += changeAmount;
        }
        if (healthChange == null) { healthChange = new UnityEvent(); }
        healthPercent = (float)health / (float)maxHealth;
        Debug.Log("current health is " + health);
        Debug.Log(healthPercent);
        healthChange.Invoke();
    }
    void Die()
    {
        GameStateSwitcher.Instance.GameOver();
    }

}