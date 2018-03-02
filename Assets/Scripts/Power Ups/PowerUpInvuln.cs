using UnityEngine;
using System.Collections;

class PowerUpInvuln : PowerUp
{
	public float invulnDurationSeconds = 5f;
	public GameObject invulnParticles;
	private GameObject invulnParticlesInstance;

	protected override void PowerUpPayload ()     // Checklist item 1
	{
		base.PowerUpPayload ();
		Cat.SetInvulnerability (false);
		if (invulnParticles != null)
		{
			invulnParticlesInstance = Instantiate (invulnParticles, Catapult.gameObject)
	}

	protected override void PowerUpHasExpired ()     // Checklist item 2
	{
		if (powerUpState == PowerUpState.IsExpiring)
		{
			return;
		}
		Cat.SetInvulnerability (false);
		if (invulnParticlesInstance != null)
		{
			Destroy (invulnParticlesInstance);
		}
		base.PowerUpHasExpired ();
	}

	private void Update ()                            // Checklist item 3
	{
		if (powerUpState == PowerUpState.IsCollected)
		{
			invulnDurationSeconds -= Time.deltaTime;
			if (invulnDurationSeconds < 0)
			{
				PowerUpHasExpired ();
			}
		}
	}
}
