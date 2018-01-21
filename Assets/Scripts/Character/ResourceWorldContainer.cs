using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceWorldContainer : MonoBehaviour {

	[Header("Container Parameters")]
	[Tooltip("The maximum number slots to display. 0 for infinite")]
	public int maxSlotCount = 0;
	[Tooltip("How big (in world units) is each slot")]
	public float slotWidth = 0.32f;
	[Tooltip("How much space should be left between slots (right side only)")]
	public float slotSpacing = 0.2f;
	[Header("Animation Parameters")]
	[Tooltip("Curve describing the animation for picking up the object")]
	public AnimationCurve pickupCurve;
	[Tooltip("How much time should the pick up animations take")]
	public float pickupTime;
	[Tooltip("Curve describing the animation for dropping the object")]
	public AnimationCurve dropCurve;
	[Tooltip("How much time should the drop animations take")]
	public float dropTime;


	GameObject _container; // Created to hold the objects added to this container
	List<GameObject> _slots = new List<GameObject>(); // Track the objects held by this container

	void Start()
	{
		_container = GameObject.Instantiate (new GameObject());
		_container.name = "inventory_sliding_container";
		_container.transform.parent = transform;
		_container.transform.localPosition = new Vector3();
	}

	void Update()
	{
		UpdatePositions ();
	}

	float getSlotOffset(int slotIndex)
	{
		return (slotIndex * slotWidth) + (slotIndex * slotSpacing);
	}

	void UpdatePositions()
	{
		// Center the container (smooth)
		float containerWidth = getSlotOffset(_slots.Count);
		float targetXOff = (-containerWidth * 0.5f) + ((slotWidth + slotSpacing) * 0.5f);

		_container.transform.localPosition = new Vector3(Mathf.Lerp (_container.transform.localPosition.x, targetXOff, Time.deltaTime * 2.0f), 0, 0);

		// Update the position of the elements inside the slots
		for(int i=0; i < _slots.Count; i++) {
			GameObject obj = _slots [i];
			obj.transform.localPosition = new Vector3 (getSlotOffset(i), 0, 0);
		}
	}

	IEnumerator PickupCoroutine(GameObject obj, int targetIndex)
	{
		obj.transform.parent = _container.transform;
		float startTime = Time.time;
		float timePerc = 0;
		bool added = false;
		Vector3 targetPos = new Vector3 (getSlotOffset (targetIndex), 0, 0);
		Vector3 startPos = obj.transform.localPosition;
		do{
			timePerc = (Time.time - startTime) / pickupTime;

			obj.transform.localPosition = Vector3.LerpUnclamped(startPos, targetPos, pickupCurve.Evaluate(timePerc));

			if (timePerc >= 0.5f && !added)
			{
				_slots.Add (obj);
				added= true;
			}

			yield return null;
		} while( timePerc <= 1.0f);
	}

	public void PlaceInContainer(GameObject obj)
	{
		IEnumerator coroutine = PickupCoroutine (obj, _slots.Count);
		StartCoroutine (coroutine);
	}

	IEnumerator DropCoroutine(GameObject obj, Vector3 targetLocation)
	{
		obj.transform.parent = null;
		float startTime = Time.time;
		float timePerc = 0;
		bool removed = false;
		Vector3 startPos = obj.transform.position;
		do {
			timePerc = (Time.time - startTime) / dropTime;

			obj.transform.position = Vector3.LerpUnclamped(startPos, targetLocation, dropCurve.Evaluate(timePerc));

			if (timePerc >= 0.5f && !removed)
			{
				removed = true;
				_slots.Remove (obj);
			}

			yield return null;

		} while (timePerc <= 1.0f);
	}

	public void RemoveFromContainer(GameObject obj, Vector3 dropTarget)
	{
		IEnumerator coroutine = DropCoroutine (obj, dropTarget);
		StartCoroutine (coroutine);
	}
}
