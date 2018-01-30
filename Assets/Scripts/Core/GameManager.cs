using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject avatar;
    public List<IHoldable> holdables = new List<IHoldable>();
	public List<IInteractable> interactables = new List<IInteractable>();

	public IInteractable GetNearestInteractable(Transform p)
	{
		float dist = float.MaxValue;
		IInteractable ret = null;
		foreach (IInteractable i in interactables) {
			float check = Vector3.Distance (p.position, i.gameObject.transform.position);

			if (check < dist) {
				dist = check;
				ret = i;
			}
		}

		return ret;
	}
}
