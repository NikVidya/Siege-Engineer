using UnityEngine;
using System.Collections;

public class PowerUpHealth : PowerUp
{

	public int healthBonus = 20;

	protected override void PowerUpPayload()  // Checklist item 1
	{
		base.PowerUpPayload();

		// Payload is to give some health bonus
		Player.SetHealthAdjustment(healthBonus);      
	}
}
}

