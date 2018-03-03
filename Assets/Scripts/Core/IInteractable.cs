using System;
using UnityEngine;

public enum InteractableType
{
	REPAIR = 0,
    PICKUP = 1,
    UPGRADE_AREA = 2
}
public enum InteractionState {
	Ready,
	Activated,
	Disabled
}
public interface IInteractable
{
	void OnInteract(CharacterInteraction instigator);
    void OnFocus(CharacterInteraction focuser);
    void OnDefocus(CharacterInteraction focuser);
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