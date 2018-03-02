﻿using System;
using UnityEngine;

public enum InteractionPriority
{
	STRUCTURE = 0,
    RESOURCE = 1,
    UPGRADE_AREA = 2
}
public enum InteractionState {
	Ready,
	Activated,
	Disabled
}
public interface IInteractable
{
	void OnInteract(CharacterInteraction instigator, CharacterInteraction.KeyState state);
    void OnFocus(CharacterInteraction focuser);
    void OnDefocus(CharacterInteraction focuser);
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