using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityUpgrade : BaseUpgrade {

	[Space(10)]
	[Header("Upgrade Parameters")]
	[Tooltip("The amount to change the health of the character by")]
	bool isInvincible = false;
	public float currentHealth = maxHealth - ChangeHealth;
	public int health;
	public float invincibilityTimer = 60;
	public float invincibilityExpire = 0;



	public override void ApplyUpgrade (GameObject playerObject)
	{
		Debug.Log ("Applied an Upgrade!");
		List<IInteractable> structures = GameManager.Instance.Interactables [InteractionPriority.STRUCTURE];
		foreach (IInteractable structure in structures) {
			Damageable damageableComponent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null && isInvincible == true) {
				damageableCompontent.maxHealth = 100;
				invincibilityExpire=Time.time+invincibilityTimer;



			}

		}
	}

	public void SetInvincible(){
		isInvincible = true;
	}

	public void SetDamagable(){
		isInvincible = false;
	}



	public override void ChangeHealth(int changeAmount)
	{
		health += changeAmount;
		health = Mathf.Clamp (health, 0, maxHealth);
	}



	public override void RemoveUpgrade (GameObject playerObject)
	{
		Debug.Log ("Removed an Upgrade!");
		List<IInteractable> structures = GameManager.Instance.Interactables [InteractionPriority.STRUCTURE];
		foreach (IInteractable structure in structures) {
			Damageable damageableCompontent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null && isInvincible == false && Time.time == invincibilityExpire) {
					damageableCompontent.maxHealth = currentHealth;
					
			}

		}
	}
}
