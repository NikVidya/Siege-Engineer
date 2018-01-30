using System;
using UnityEngine;

public enum InteractionState {
	Ready,
	Activated,
	Disabled
}
public interface IInteractable
{
	void OnInteract(CharacterInteraction.KeyState state);
	void SetBlocked(bool blocked);
	InteractionState interactState { get; set; }
	GameObject gameObject { get; }
}

public enum HoldState
{
	Held,
	Dropped
}
public interface IHoldable : IInteractable
{
	void Drop();
	HoldState heldState { get; set; }
}

