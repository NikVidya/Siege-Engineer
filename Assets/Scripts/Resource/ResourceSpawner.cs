using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour {

    // Note! Resource instance should not actually be destroyed. They should be deactivated and reactivated

    [Tooltip("The amount of time it takes this resource type to 'respawn' after it's associated resource was destroyed or consumed.")]
    public float respawnTime = 1.0f;

    [Tooltip("The resource prefab to 'spawn' after the timer.")]
    public GameObject resourcePrefab = null;

    [Tooltip("Should this resource be spawned immediately.")]
    public bool spawnOnMatchStart = false;

    [Tooltip("The location that this spawner should spawn the resource at.")]
    public Vector3 spawnPosition = new Vector3(0.0f,1.0f,0.0f);

    GameObject resourceInstance;
    private bool resourceIsSpawned = false;
    private float timeSinceLastDespawn = 0.0f;


    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.TransformPoint(spawnPosition), "spawn_icon.png", true);
    }

    // Use this for initialization
    void Start () {
		if(resourcePrefab && !resourceInstance)
        {   // Resource instance needs to be created
            resourceInstance = GameObject.Instantiate(resourcePrefab, transform.TransformPoint(spawnPosition), new Quaternion(), null);
            GameManager.Instance.activeResources.Add(resourceInstance);
        }
        if (!resourceIsSpawned && spawnOnMatchStart)
        {
            SpawnResource();
        }
        else if (!resourceIsSpawned)
        {
            resourceInstance.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (!resourceIsSpawned && timeSinceLastDespawn >= respawnTime)
        {
            SpawnResource();
        }
        else if(!resourceIsSpawned)
        {
            timeSinceLastDespawn += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.activeResources.Remove(resourceInstance);
    }

    void SpawnResource()
    {
        resourceInstance.SetActive(true);
        resourceInstance.transform.position = transform.TransformPoint(spawnPosition);
    }
}
