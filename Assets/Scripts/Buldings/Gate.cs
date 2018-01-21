using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO
	1. Have the gate's health correspond to the healthbar graphic
		- Alternatively, ditch the healthbar and use gate damage sprites
	2. Player can repair the gate
	3. Placeholder sprites
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
    public int health = 100;
    [Tooltip("Whether the building is currently repairing")]
    public bool repairState = false;
    public Object HealthBar;
    //private
    void Start()
    {
        InvokeRepeating("DecrementHealth", 5, 1);
    }

    void Update()
    {
        if (repairState)
        {
            health += healthRepairRate;
        }
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    /* 
	Used to begin repairing the building
	*/
    void Repair()
    {
        if (!repairState)
        {
            repairState = true;
        }
    }

    /*
	Used to stop repairing the building
	 */
    void StopRepairing()
    {
        if (repairState)
        {
            repairState = false;
        }
    }

    void DecrementHealth()
    {
        if (!repairState)
        {
            health -= healthDecreaseRate;
        }
    }
    void Die()
    {
        print("walls died");
    }
}