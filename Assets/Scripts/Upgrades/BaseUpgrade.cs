
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseUpgrade : MonoBehaviour
{
	[Header("Upgrade Info")]
	[Tooltip("The displayed name of this upgrade")]
	public string UpgradeName;
	[Tooltip("The currency cost of purchasing this upgrade")]
	public float UpgradeCost;

	abstract public void ApplyUpgrade(GameObject playerObject);
	abstract public void RemoveUpgrade(GameObject playerObject);
}

