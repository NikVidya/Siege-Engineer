using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour {

    [Header("Pickup Parameters")]
    [Tooltip("Maximum distance that a resource can be picked up")]
    public float pickupDistance = 1.0f;


    private List<GameObject> inventory; // List of items currently held by the player

    private void Start()
    {

    }

    void Update () {
        GameObject[] allResources = GameObject.FindGameObjectsWithTag(Constants.Tags.RESOURCE);
        float minDist = float.MaxValue;
        GameObject closestResource = null;
        foreach(GameObject resource in allResources)
        {
            float distance = Vector3.Distance(transform.position, resource.transform.position);
            if (distance < minDist && distance < pickupDistance)
            {
                minDist = distance;
                closestResource = resource;
            }
            else
            {
                Resource resourceComponent = resource.GetComponent<Resource>();
                if (resourceComponent)
                {
                    resourceComponent.ShowPrompt(false);
                }
            }
        }
        
        if (closestResource)
        {
            Resource resourceComponent = closestResource.GetComponent<Resource>();
            if (resourceComponent)
            {
                resourceComponent.ShowPrompt(true);
            }
        }
	}
}
