using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject avatar;

    public List<IInteractable>[] interactables;

    private void Start()
    {
        interactables = new List<IInteractable>[InteractionPriority.GetValues(typeof(InteractionPriority)).Length];
        for(int i=0; i < interactables.Length; i++)
        {
            interactables[i] = new List<IInteractable>();
        }
    }

    public void RegisterInteractable(IInteractable interactable, InteractionPriority priority)
    {
        interactables[(int)priority].Add(interactable);
    }
    public void DeRegisterInteractable(IInteractable interactable, InteractionPriority priority)
    {
        interactables[(int)priority].Remove(interactable);
    }

    public IInteractable GetNearestInteractableInRange(Transform t, InteractionPriority priority, float range)
    {
        float dist = float.MaxValue;
        IInteractable ret = null;
        foreach (IInteractable i in interactables[(int)priority])
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
        IInteractable[] ret = new IInteractable[interactables.Length];

        for(int i = 0; i < interactables.Length; i++)
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
}
