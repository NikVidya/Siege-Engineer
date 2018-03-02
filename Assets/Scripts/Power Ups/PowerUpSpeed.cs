using UnityEngine;
using System.Collections;

public class PowerUpSpeed : PowerUp
{
	[Range(1.0f, 4.0f)]
	public float speedMultiplier = 2.0f;
	public float speedDurationSeconds = 5f;


	protected override void PowerUpPayload()          // Checklist item 1
	{
		base.PowerUpPayload();
		player.SetSpeedBoostOn(speedMultiplier);
	}

	protected override void PowerUpHasExpired()       // Checklist item 2
	{
		if (powerUpState == PowerUpState.IsExpiring)
		{
			return;
		}
		base.PowerUpHasExpired();
		Player.SetSpeedBoostOff();
	}

	private void Update ()                            // Checklist item 3
	{
		if (powerUpState == PowerUpState.IsCollected)
		{
			speedDurationSeconds -= Time.deltaTime;
			if (speedDurationSeconds < 5f)
			{
				PowerUpHasExpired ();
			}
		}
	}
}



