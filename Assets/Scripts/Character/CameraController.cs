using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject objectToTrack;
	public float trackSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}

	void LateUpdate () {
		Vector3 diff = objectToTrack.transform.position - transform.position;
		diff = Vector3.ClampMagnitude (diff, trackSpeed);
		diff.z = 0;
		transform.position = transform.position + diff;
	}
}
