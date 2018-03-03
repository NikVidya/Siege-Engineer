using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : BaseUpgrade {

	[Space(10)]
	[Header("Upgrade Parameters")]
	[Tooltip("The amount to change the max speed of the character by")]
	public float maxSpeedMod = 0.05f;
	public float speedTimer = 60;
	public float speedExpire = 0;



	public override void ApplyUpgrade (GameObject playerObject)
	{
		Debug.Log ("Applied an Upgrade!");
		CharacterMovement movementCompontent = playerObject.GetComponent<CharacterMovement> ();
		if (movementComponent != null) {
			movementComponent.maxSpeed += maxSpeedMod;
			speedExpire=Time.time+speedTimer;

		}
	}

	public override void RemoveUpgrade (GameObject playerObject)
	{
		CharacterMovement movementCompontent = playerObject.GetComponent<CharacterMovement> ();
		if (movementCompontent != null && Time.time == speedExpire) {
			movementCompontent.maxSpeed -= maxSpeedMod;
		}
	}
}
