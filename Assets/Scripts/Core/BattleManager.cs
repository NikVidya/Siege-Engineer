using UnityEngine;
using System.Collections;

public class BattleManager : Singleton<BattleManager>
{
	[Header("General")]
	[Tooltip("The area of battle")]
	public Rect battleArea;
	[Header("Bombardment")]
	[Tooltip("Prefab to spawn as the bombardment")]
	public GameObject bombardment;
	[Tooltip("Time between bombardments")]
	public float bombardmentTime = 5.0f;
	[Tooltip("Deviation of bombardment timer")]
	public float bombardmentDeviation = 5.0f;

	float lastBombardmentStartTime = 0.0f;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube (battleArea.center, battleArea.size);
	}

	public void SpawnBombardment()
	{
		GameObject spawned = GameObject.Instantiate (bombardment);
		spawned.transform.position = transform.position + new Vector3 (Random.Range (battleArea.xMin, battleArea.xMax), Random.Range (battleArea.yMin, battleArea.yMax), 0.0f);
	}

	void Update()
	{
		if (Time.time - lastBombardmentStartTime >= (bombardmentTime + Random.Range(-(bombardmentDeviation*0.5f),bombardmentDeviation*0.5f))) {
			SpawnBombardment ();
			lastBombardmentStartTime = Time.time;
		}
	}
}

