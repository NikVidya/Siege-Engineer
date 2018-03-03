using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityUpgrade : TimedUpgrade {
	
	[Header("Upgrade Parameters")]
	[Tooltip("The length of time to remain invincible in seconds")]
	public float invincibilityLength = 60;

	Dictionary<GameObject, int> storedHealth = new Dictionary<GameObject, int>();

	// Thanks to this being a GameObject, we can use the Update tick within the upgrades!
	void Update()
	{
		// As long as this upgrade is active, set all the structures to max health
		if (isActive) {
			// Get all the structures in the scene (using the list the GameManager keeps)
			List<IInteractable> structures = GameManager.Instance.Interactables [(int)InteractionPriority.STRUCTURE];
			foreach (IInteractable structure in structures) {
				// For each structure, get it's damageable component
				Damageable damageableComponent = structure.gameObject.GetComponent<Damageable> ();
				if (damageableComponent != null) {
					// If it had a damageable component
					// Set it's health to max
					damageableComponent.health = damageableComponent.maxHealth;
				}
			}
		}
	}
		
	protected override void OnApplyUpgrade (GameObject playerObject)
	{
		// Get all the structures in the scene (using the list the GameManager keeps)
		List<IInteractable> structures = GameManager.Instance.Interactables [(int)InteractionPriority.STRUCTURE];
		foreach (IInteractable structure in structures) {
			// For each structure, get it's damageable component
			Damageable damageableComponent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableComponent != null) {
				// If it had a damageable component
				// Store the amount of health it had before so we can restore it when the upgrade turns off
				storedHealth.Add(damageableComponent.gameObject, damageableComponent.health);
				// Set it's health to max
				damageableComponent.health = damageableComponent.maxHealth;
			}
		}
	}

	protected override void OnRemoveUpgrade (GameObject playerObject)
	{
		// Return all the damageables in the scene to their original health
		List<IInteractable> structures = GameManager.Instance.Interactables [(int)InteractionPriority.STRUCTURE];
		foreach (IInteractable structure in structures) {
			Damageable damageableCompontent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null) {
					damageableCompontent.health = storedHealth[damageableCompontent.gameObject];
			}
		}
	}
}
