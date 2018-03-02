﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

	public CharacterInteraction interactionComponent;

	public List<IInteractable>[] Interactables {
		get {
			if (_interactables == null) {
				InitializeInteractables ();
			}
			return _interactables;
		}
	}
	public List<IInteractable>[] _interactables;
	public List<Damageable> damagebales = new List<Damageable>();

	public List<IBombable> bombables = new List<IBombable>();

	public void RegisterDamageable(Damageable damageable)
	{
		damagebales.Add (damageable);
	}

	public void DeRegisterDamageable(Damageable damageable)
	{
		damagebales.Remove (damageable);
	}

	public void RemoveDamageDeBuff(Catapult defensiveStructure)
	{
		foreach (Damageable damageable in damagebales) {
			damageable.damageDeBuff -= defensiveStructure.damageDebuf;
		}
	}

	public void AddDamageDeBuff(Catapult defensiveStructure)
	{
		foreach (Damageable damageable in damagebales) {
			damageable.damageDeBuff += defensiveStructure.damageDebuf;
		}
	}

	void InitializeInteractables()
	{
		_interactables = new List<IInteractable>[InteractionPriority.GetValues(typeof(InteractionPriority)).Length];
		for(int i=0; i < _interactables.Length; i++)
		{
			_interactables[i] = new List<IInteractable>();
		}
	}

    private void Start()
    {
		
    }

    public void ConsumeResource(GameResource resource)
    {
		interactionComponent.InventoryComponent.DropHeld(resource);
        resource.gameObject.SetActive(false);
    }

    public void RegisterInteractable(IInteractable interactable, InteractionPriority priority)
	{
        Interactables[(int)priority].Add(interactable);
    }
    public void DeRegisterInteractable(IInteractable interactable, InteractionPriority priority)
    {
        Interactables[(int)priority].Remove(interactable);
    }

    public IInteractable GetNearestInteractableInRange(Transform t, InteractionPriority priority, float range)
    {
        float dist = float.MaxValue;
        IInteractable ret = null;
        foreach (IInteractable i in Interactables[(int)priority])
        {
            float checkDist = Vector3.Distance(t.position, i.gameObject.transform.position);
            if (checkDist < dist && checkDist < range && i.InteractState == InteractionState.Ready)
            {
                dist = checkDist;
                ret = i;
            }
        }
        return ret;
    }

    public IInteractable[] GetNearestInteractableInRange(Transform t, float range)
    {
        IInteractable[] ret = new IInteractable[Interactables.Length];

        for(int i = 0; i < Interactables.Length; i++)
        {
            ret[i] = GetNearestInteractableInRange(t, (InteractionPriority)i, range);
        }

        return ret;
    }

    public IInteractable GetHighestPriorityNearestInteractableInRange(Transform t, float range)
    {
        IInteractable[] dat = GetNearestInteractableInRange(t, range);
        for (int i = 0; i < dat.Length; i++)
        {
            if (dat[i] != null)
            {
                return dat[i];
            }
        }
        return null;
    }

    public List<GameResource> GetResourcesInRange(Transform t, float range)
    {
        List<GameResource> ret = new List<GameResource>();
		foreach(IInteractable interact in Interactables[(int)InteractionPriority.RESOURCE])
        {
            GameResource gr = interact as GameResource; // Try and cast down the tree to a GameResource
			if (gr != null && ((IHoldable)gr).HeldState != HoldState.Held && Vector3.Distance(gr.transform.position, t.position) <= range)
            {   // This interactable was a resource, and is close enough to be included
                ret.Add(gr);
            }
        }

        return ret;
    }

	public void RegisterBombable(IBombable bombable)
	{
		bombables.Add (bombable);
	}

	public void DeRegisterBombable(IBombable bombable)
	{
		bombables.Remove(bombable);
	}

	public List<IBombable> GetBombablesInRange(Transform t, float range)
	{
		List<IBombable> ret = new List<IBombable> ();
		foreach (IBombable bombable in bombables) {
			if (bombable.GetDistanceToTransform (t) < range) {
				ret.Add (bombable);
			}
		}
		return ret;
	}

    public void DisableAvatar()
    {
        interactionComponent.DisableInteraction();
        CharacterMovement movementComponent = interactionComponent.gameObject.GetComponent<CharacterMovement>();
        if (movementComponent != null)
        {
            movementComponent.DisableMovement();
        }
    }

    public void EnableAvatar()
    {
        interactionComponent.EnableInteraction();
        CharacterMovement movementComponent = interactionComponent.gameObject.GetComponent<CharacterMovement>();
        if (movementComponent != null)
        {
            movementComponent.EnableMovement();
        }
    }
}
