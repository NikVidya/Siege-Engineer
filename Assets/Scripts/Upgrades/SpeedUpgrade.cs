using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : TimedUpgrade {
	
	[Header("Upgrade Parameters")]
	[Tooltip("The amount to change the max speed of the character by")]
	public float maxSpeedMod = 0.05f;

	protected override void OnApplyUpgrade (GameObject playerObject)
	{
		CharacterMovement movementCompontent = playerObject.GetComponent<CharacterMovement> ();
		if (movementCompontent != null) {
			movementCompontent.maxSpeed += maxSpeedMod;
		}
	}

	protected override void OnRemoveUpgrade (GameObject playerObject)
	{
		CharacterMovement movementCompontent = playerObject.GetComponent<CharacterMovement> ();
		if (movementCompontent != null) {
			movementCompontent.maxSpeed -= maxSpeedMod;
		}
	}
}
