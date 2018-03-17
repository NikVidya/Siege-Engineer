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

    Animator animator;
    private const string ANIMATOR_HAS_RESOURCE_TAG = "hasResource";

    void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.TransformPoint(spawnPosition), 0.1f);
    }

    // Use this for initialization
    void Start () {
        // Acquire a reference to an animator component if there is one to be had
        animator = GetComponent<Animator>();

		if(resourcePrefab && !resourceInstance)
        {   // Resource instance needs to be created
            resourceInstance = GameObject.Instantiate(resourcePrefab, transform.TransformPoint(spawnPosition), new Quaternion(), null);
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
		if (!resourceInstance.activeInHierarchy && resourceIsSpawned) {
			resourceIsSpawned = false;
			timeSinceLastDespawn = 0.0f;
            if(animator != null)
            {
                animator.SetBool(ANIMATOR_HAS_RESOURCE_TAG, false);
            }
		}
    }

    void SpawnResource()
    {
		resourceIsSpawned = true;
        resourceInstance.SetActive(true);
        resourceInstance.transform.position = transform.TransformPoint(spawnPosition);
        if (animator != null)
        {
            animator.SetBool(ANIMATOR_HAS_RESOURCE_TAG, true);
        }
    }
}
