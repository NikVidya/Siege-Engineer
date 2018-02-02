using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Damageable))]
public class Catapult : MonoBehaviour
{
	[Tooltip("Percentage of damage blocked when this component is alive component")]
	public float damageDebuf = 0.01f;

	bool isAlive = true;
	Damageable damageableComponent;

	// Use this for initialization
	void Start ()
	{
		damageableComponent = GetComponent<Damageable> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (damageableComponent.health <= 0.0f && isAlive) {
			GameManager.Instance.RemoveDamageDeBuff (this);
			isAlive = false;
		} else if (damageableComponent.health > 0.0f && !isAlive) {
			isAlive = true;
			GameManager.Instance.AddDamageDeBuff (this);
		}
	}

	void PeriodicHealthChange()
	{
		 if (periodicDamage)
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
}

