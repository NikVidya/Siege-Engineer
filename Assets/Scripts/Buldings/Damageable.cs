using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO
	1. Have the Damageable's health correspond to the healthbar graphic
		- Alternatively, ditch the healthbar and use Damageable damage sprites
	2. Placeholder sprites
 */
[RequireComponent(typeof(Transform))]
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
    public int health;
    [HideInInspector]
    public bool repairState = false;
    //private
    void Start()
    {
        health = maxHealth;
        if (periodicDamage)
        {
            InvokeRepeating("PeriodicHealthChange", gracePeriod, healthDecreaseRate);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    void PeriodicHealthChange()
    {
        if (repairState)
        {
            ChangeHealth(healthRepairAmount);
        }
        else
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
        Debug.Log("Health changed " + (health - healthPrevious));
        Debug.Log("Current Damageable health is " + health);
    }
    void Die()
    {
        Debug.Log("object died");
    }
}