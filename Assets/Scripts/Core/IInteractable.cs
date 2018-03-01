using System;
using UnityEngine;

public enum InteractableType
{
	REPAIR = 0,
    PICKUP = 1
}
public enum InteractionState {
	Ready,
	Activated,
	Disabled
}
public interface IInteractable
{
	void OnInteract(CharacterInteraction instigator);
	InteractionState InteractState { get; set; }
    InteractableType InteractableType { get; set; }
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
	HoldState HeldState { get; set; }
}