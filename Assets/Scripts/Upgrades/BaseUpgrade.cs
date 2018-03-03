
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseUpgrade : MonoBehaviour
{
	[Header("Upgrade Info")]
	[Tooltip("The displayed name of this upgrade")]
	public string UpgradeName;
	[Tooltip("The currency cost of purchasing this upgrade")]
	public float UpgradeCost;

	public bool IsApplied {
		get; private set;
	}

	public virtual void ApplyUpgrade(GameObject playerObject){
		if (!IsApplied) {
			IsApplied = true;
			OnApplyUpgrade (playerObject);
		}
	}
	public virtual void RemoveUpgrade(GameObject playerObject){
		if (IsApplied) {
			IsApplied = false;
			OnRemoveUpgrade (playerObject);
		}
	}

	abstract protected void OnApplyUpgrade(GameObject playerObject);
	abstract protected void OnRemoveUpgrade(GameObject playerObject);
}

