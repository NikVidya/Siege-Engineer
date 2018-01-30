using UnityEngine;
using System.Collections;

public class CharacterInteraction : MonoBehaviour
{
	[Header("Interaction Parameters")]
	[Tooltip("How long the key has to be pressed to trigger a 'held' interaction")]
	public float holdTime = 0.5f;
	public enum KeyState {
		Pressed,
		Held,
		Released
	}
	KeyState curKeyState = KeyState.Released;
	KeyState oldKeyState = KeyState.Released;
	float keyPressedStartTime = 0.0f;


	void Update ()
	{
		if (Input.GetButtonDown (Constants.InputNames.INTERACT) && curKeyState == KeyState.Released) {
			curKeyState = KeyState.Pressed;
			keyPressedStartTime = 0.0f;
		} else if (Input.GetButton (Constants.InputNames.INTERACT) && curKeyState == KeyState.Pressed) {
			keyPressedStartTime += Time.deltaTime;
		} else if (Input.GetButton (Constants.InputNames.INTERACT) && curKeyState == KeyState.Pressed && keyPressedStartTime >= holdTime) {
			curKeyState = KeyState.Held;
		} else if (Input.GetButtonUp (Constants.InputNames.INTERACT) && (curKeyState == KeyState.Pressed || curKeyState == KeyState.Held)) {
			curKeyState = KeyState.Released;
		}

		if (oldKeyState != curKeyState) {
			OnKeyStateChange (curKeyState);
			oldKeyState = curKeyState;
		}
	}

	void OnKeyStateChange(KeyState keyState)
	{
		Debug.LogFormat ("KeyState changed: {0}", keyState);
		IInteractable interactable = GameManager.Instance.GetNearestInteractable (gameObject.transform);
		if (interactable != null) {
			interactable.OnInteract (keyState);
		}
	}
}

