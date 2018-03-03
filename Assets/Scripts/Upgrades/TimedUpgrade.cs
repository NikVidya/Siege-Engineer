using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedUpgrade : BaseUpgrade {

	[Header("Upgrade Duration")]
	[Tooltip("Duration of upgrade in seconds")]
	public float UpgradeDuration = 15.0f;

	protected bool isActive = false;

	// Upgrades are instanciated GameObjects, so regular MonoBehaviour functions are valid, like Coroutines
	IEnumerator Timer(GameObject playerObject, float delay)
	{
		yield return new WaitForSeconds (delay);
		RemoveUpgrade (playerObject);
	}

	public override void ApplyUpgrade (GameObject playerObject)
	{
		isActive = true;
		base.ApplyUpgrade (playerObject);
		StartCoroutine (Timer (playerObject, UpgradeDuration));
	}

	public override void RemoveUpgrade(GameObject playerObject)
	{
		isActive = false;
		base.RemoveUpgrade (playerObject);
	}
}
