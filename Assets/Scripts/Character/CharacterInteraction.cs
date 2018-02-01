using UnityEngine;
using System.Collections;

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

    public enum KeyState
    {
        Pressed,
        Held,
        Released
    }
    KeyState curKeyState = KeyState.Released;
    KeyState oldKeyState = KeyState.Released;
    float keyPressedStartTime = 0.0f;
	bool canDrop = true;

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
        _inv = gameObject.GetComponent<CharacterInventory>();
        if (!interactionPrompt)
        {
            Debug.LogError("Character Interaction does not have an interaction prompt!");
        }
        interactionPrompt.SetActive(false);
    }

    void ShowPrompt(GameObject target)
    {
        interactionPrompt.SetActive(true);
        interactionPrompt.transform.position = target.transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        interactionPrompt.transform.parent = target.transform;
    }
    void HidePrompt()
    {
        interactionPrompt.SetActive(false);
    }

    IInteractable FocusCloseHoldable()
    {
        IInteractable closest = GameManager.Instance.GetHighestPriorityNearestInteractableInRange(transform, interactDistance);
        // Found closest if it exists
        if (closest != null)
        {   // Show the prompt
            ShowPrompt(closest.gameObject);
        }
        else
        {   // Hide the prompt
            HidePrompt();
        }

        return closest;
    }

    void Update()
    {
        IInteractable closest = FocusCloseHoldable();
        if (Input.GetButtonDown(Constants.InputNames.INTERACT) && curKeyState == KeyState.Released)
        {
            curKeyState = KeyState.Pressed;
            keyPressedStartTime = 0.0f;
			canDrop = true;
        }
        else if (Input.GetButton(Constants.InputNames.INTERACT) && curKeyState == KeyState.Pressed && keyPressedStartTime < holdTime)
        {
            keyPressedStartTime += Time.deltaTime;
        }
        else if (Input.GetButton(Constants.InputNames.INTERACT) && curKeyState == KeyState.Pressed && keyPressedStartTime >= holdTime)
        {
            curKeyState = KeyState.Held;
        }
        else if (Input.GetButtonUp(Constants.InputNames.INTERACT) && (curKeyState == KeyState.Pressed || curKeyState == KeyState.Held))
        {
            curKeyState = KeyState.Released;
        }

        if (oldKeyState != curKeyState)
        {
            OnKeyStateChange(curKeyState, closest);
            oldKeyState = curKeyState;
        }
    }

    void OnKeyStateChange(KeyState keyState, IInteractable interactable)
    {
		if (interactable != null) {
			interactable.OnInteract (this, keyState);
			canDrop = false;
		} else if (canDrop && keyState == KeyState.Released){
			InventoryComponent.DropFirstHeld ();
		}
    }
}

