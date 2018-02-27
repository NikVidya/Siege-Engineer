using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySpawner : MonoBehaviour {

	// public
	[Tooltip ("Amount of time between spawns")]
	public float spawnTime = 10;
	[Tooltip ("Amount of resource instances possible at a given time.")]
	public int currencyMaxSpawnAmount = 5;
	[Tooltip ("Possible spawn positions for the currency pickups.")]
	public Vector3[] spawnPositions = new Vector3[5];
	[Tooltip ("The prefab to spawn in a random location")]
	public GameObject currencyPrefab = null;
	// private
	private float spawnTimer;
	private GameObject[] instanceList;
	private int instanceIteration = 0;

	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		for (int i = 0; i < spawnPositions.Length; i++) {
			Gizmos.DrawWireSphere (transform.TransformPoint (spawnPositions[i]), 0.1f);
		}
	}

	void Start () {
		spawnTimer = spawnTime;
		if (currencyMaxSpawnAmount < spawnPositions.Length) {
			Debug.LogWarning ("Amount of currency instances greater than possible spawn positions. Clamped to amount of spawn positions");
			Mathf.Clamp (currencyMaxSpawnAmount, 0, spawnPositions.Length);
		}

		instanceList = new GameObject[currencyMaxSpawnAmount];

		for (int i = 0; i < instanceList.Length; i++) {
			if (currencyPrefab) { // Resource instance needs to be created
				instanceList[i] = GameObject.Instantiate (currencyPrefab, transform.TransformPoint (spawnPositions[i]), new Quaternion (), null);
			}
			instanceList[i].SetActive (false);
		}

	}

	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0) {
			SpawnCurrency ();
			spawnTimer = spawnTime;
		}
	}

	void SpawnCurrency () {
		if (!instanceList[instanceIteration].activeInHierarchy) {
			instanceList[instanceIteration].SetActive (true);
		}

		SetRandomLocation (instanceList[instanceIteration]);

		instanceIteration++;
		if (instanceIteration == currencyMaxSpawnAmount) {
			instanceIteration = 0;
		}
	}

	void SetRandomLocation (GameObject o) {
		o.transform.position = spawnPositions[Random.Range (0, spawnPositions.Length - 1)];
	}
}