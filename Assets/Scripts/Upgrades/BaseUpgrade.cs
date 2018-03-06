
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseUpgrade : MonoBehaviour
{
	[Header("Upgrade Info")]
	[Tooltip("The displayed name of this upgrade")]
	public string UpgradeName;
	[Tooltip("The currency cost of purchasing this upgrade")]
	public float UpgradeCost;
	[Tooltip("The Prefab containing the UI hierarcy for the list entry in the shop window")]
	[SerializeField]
	protected GameObject EntryListTemplate;

	protected GameObject _entryListInstance;

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

	public virtual GameObject GetListEntryObject()
	{
		_entryListInstance = GameObject.Instantiate (EntryListTemplate);
		UpgradeListEntry listEntryComponent = _entryListInstance.GetComponent<UpgradeListEntry> ();
		if (listEntryComponent != null)
		{
			listEntryComponent.DisplayNameText.text = UpgradeName;
			listEntryComponent.CostText.text = string.Format ("${0:F2}", UpgradeCost);
		}
		return _entryListInstance;
	}

	public void SetEntrySelected()
	{
		if (_entryListInstance != null) {
			Button btn = _entryListInstance.GetComponent<Button> ();
			if (btn != null) {
				btn.Select ();
			}
		}
	}

	abstract protected void OnApplyUpgrade(GameObject playerObject);
	abstract protected void OnRemoveUpgrade(GameObject playerObject);
}

