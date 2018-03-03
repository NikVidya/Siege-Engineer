using System;
using UnityEngine;

public enum InteractableType
{
	STRUCTURE = 0,
    RESOURCE = 1
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
	REPAIR = 0,
    PICKUP = 1,
    UPGRADE_AREA = 2
	void OnInteract(CharacterInteraction instigator);
    void OnFocus(CharacterInteraction focuser);
    void OnDefocus(CharacterInteraction focuser);