using System;
using UnityEngine;

public enum InteractionPriority
{
    RESOURCE = 0,
    STRUCTURE = 1
}
public enum InteractionState {
	Ready,
	Activated,
	Disabled
}
public interface IInteractable
{
	void OnInteract(CharacterInteraction instigator, CharacterInteraction.KeyState state);
	InteractionState InteractState { get; set; }
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

