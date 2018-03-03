using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : BaseUpgrade {

	[Space(10)]
	[Header("Upgrade Parameters")]
	[Tooltip("The amount to change the health of the character by")]
	public float HealthBonus = 10;

	public override void ApplyUpgrade (GameObject playerObject)
	{
		Debug.Log ("Applied an Upgrade!");
		List<IInteractable> structures = GameManager.Instance.Interactables [InteractionPriority.STRUCTURE];
		foreach (IInteractable structure in structures) {
			Damageable damageableCompontent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null) {
				damageableCompontent.maxHealth += HealthBonus;
			}

		}
	}

	public override void RemoveUpgrade (GameObject playerObject)
	{
		Debug.Log ("Applied an Upgrade!");
		List<IInteractable> structures = GameManager.Instance.Interactables [InteractionPriority.STRUCTURE];
		foreach (IInteractable structure in structures) {
			Damageable damageableCompontent = structure.gameObject.GetComponent<Damageable> ();
			if (damageableCompontent != null) {
				damageableCompontent.maxHealth -= HealthBonus;
			}

		}
	}
}
