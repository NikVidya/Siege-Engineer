using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : BaseUpgrade {

	[Space(10)]
	[Header("Upgrade Parameters")]
	[Tooltip("The amount to change the max speed of the character by")]
	public float maxSpeedMod = 0.05f;

	public override void ApplyUpgrade (GameObject playerObject)
	{
		Debug.Log ("Applied an Upgrade!");
		CharacterMovement movementCompontent = playerObject.GetComponent<CharacterMovement> ();
		if (movementCompontent != null) {
			movementCompontent.maxSpeed += maxSpeedMod;
		}
	}

	public override void RemoveUpgrade (GameObject playerObject)
	{
		CharacterMovement movementCompontent = playerObject.GetComponent<CharacterMovement> ();
		if (movementCompontent != null) {
			movementCompontent.maxSpeed -= maxSpeedMod;
		}
	}
}
