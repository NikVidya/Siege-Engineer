using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : BaseUpgrade {

	[Header("Upgrade Parameters")]
	[Tooltip("Amount of health to add to all damageables")]
	public int HealthBonus = 10;

	protected override void OnApplyUpgrade (GameObject playerObject)
	{
		List<IInteractable> structures = GameManager.Instance.Interactables [(int)InteractableType.REPAIR];
		foreach (IInteractable structure in structures) {
			Damageable damageableCompontent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null) {
				damageableCompontent.maxHealth += HealthBonus;
			}

		}
	}

	protected override void OnRemoveUpgrade (GameObject playerObject)
	{
		List<IInteractable> structures = GameManager.Instance.Interactables [(int)InteractableType.REPAIR];
		foreach (IInteractable structure in structures) {
			Damageable damageableCompontent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null) {
				damageableCompontent.maxHealth -= HealthBonus;
			}
		}
	}
}
