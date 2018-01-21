using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO
	1. Have the gate's health correspond to the healthbar graphic
		- Alternatively, ditch the healthbar and use gate damage sprites
	2. Placeholder sprites
 */
[RequireComponent(typeof(Transform))]
public class Gate : MonoBehaviour
{
    //public
    [Tooltip("The rate at which the gate receives constant damage")]
    public int healthDecreaseRate = 3;
    [Tooltip("The rate at which the player repairs the gate")]
    public int healthRepairRate = 10;
    [Tooltip("The health of the building - decreases at healthDecreaseRate")]
    public int maxHealth = 100;
    [HideInInspector]
    public int health;
    [HideInInspector]
    public bool repairState = false;
    //private
    void Start()
    {
        health = maxHealth;
        InvokeRepeating("ChangeHealth", 5, 1);
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

    /* 
	Used to begin repairing the building
	*/
    public void ChangeHealth()
    {
        int healthPrevious = health;
        if (repairState && health < maxHealth)
        {
            if (health > maxHealth - healthRepairRate)
            {
                health += maxHealth - health;
            } else {
                health += healthRepairRate;
            }
        }
        else
        {
            health -= healthDecreaseRate;
        }
        Debug.Log("Health changed by" + (health - healthPrevious));
        Debug.Log("Current gate health is " + health);
    }
    void Die()
    {
        Debug.Log("walls died");
    }
}