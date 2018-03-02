using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterInventory))]
public class CharacterInteraction : MonoBehaviour
{
    [Header("Interaction Parameters")]
    [Tooltip("How long the key has to be pressed to trigger a 'held' interaction")]
    public float holdTime = 0.5f;
    [Tooltip("How far away from the player can they interact with things")]
    public float interactDistance = 1.0f;
    [Tooltip("The GameObject or prefab to use as indication that an object can be picked up")]
    public GameObject interactionPrompt;
    
	bool canDrop = true;
    Dictionary<GameObject, GameObject> prompts;

    public CharacterInventory InventoryComponent { get { return _inv; } }
    CharacterInventory _inv;

    // EDITOR: Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

    private void Start()
	{
		GameManager.Instance.interactionComponent = this;
        _inv = gameObject.GetComponent<CharacterInventory>();
        if (!interactionPrompt)
        {
            Debug.LogError("Character Interaction does not have an interaction prompt!");
        }
        interactionPrompt.SetActive(false);
        prompts = new Dictionary<GameObject, GameObject>();
    }

    void AddPrompt(GameObject target, InteractableType type)
    {
        if (!prompts.ContainsKey(target))
        {
            // TODO: When art supports it, set the text on the prompt based on the type
            GameObject prompt = GameObject.Instantiate(interactionPrompt);
            prompt.SetActive(true);
            prompt.transform.position = target.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
            prompt.transform.parent = target.transform;
            prompts.Add(target, prompt);
        }
    }
    void RemovePrompt(GameObject target)
    {
        GameObject.Destroy(prompts[target]);
    }

    IInteractable[] HighlightNearestInteractables()
    {
        IInteractable[] interactables = GameManager.Instance.GetNearestInteractablesFromEachTypeInRange(gameObject.transform, interactDistance);

        foreach(IInteractable interactable in interactables)
        {
            if (interactable != null)
            {
                AddPrompt(interactable.gameObject, interactable.InteractableType);
            }
        }

        return interactables;
    }

    void Update()
    {
        IInteractable[] interactables = HighlightNearestInteractables();
        CheckIfInteracting(interactables);
    }

    void CheckIfInteracting(IInteractable[] interactablePool)
    {
        // Check the pickup key response
        if (Input.GetButtonDown(Constants.InputNames.PICKUP))
        {
            if (interactablePool[(int)InteractableType.PICKUP] == null)
            { // No pickup to interact with
                canDrop = true;
            }
            else
            {
                canDrop = false;
                interactablePool[(int)InteractableType.PICKUP].OnInteract(this);
            }
        }
        else if (Input.GetButtonUp(Constants.InputNames.PICKUP) && canDrop)
        {   // Nothing was close enough to pickup when pressing the key, releasing should drop one
            canDrop = false;
            InventoryComponent.DropFirstHeld();
        }

        // Check the repair key response
        if (Input.GetButtonUp(Constants.InputNames.REPAIR))
        {
            // Trigger a repair on the nearest repairable
            if(interactablePool[(int)InteractableType.REPAIR] != null)
            {
                interactablePool[(int)InteractableType.REPAIR].OnInteract(this);
            }
        }
    }
}

