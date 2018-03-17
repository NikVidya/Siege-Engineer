using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractionPromptIcon : MonoBehaviour {
	public enum InteractionIconType
	{
		A,
		B,
		X,
		Y
	}
	[Tooltip("Interaction icon list")]
	public Sprite[] icons;

	public SpriteRenderer spriteRenderer;

	private void Start()
	{
		if(spriteRenderer == null)
		{
			Debug.LogErrorFormat("Interaction prompt '{0}' did not have a SpriteRenderer component", gameObject.name);
		}
	}

	public void SetPromptType(InteractionIconType type)
	{
		spriteRenderer.sprite = icons[(int)type];
	}

	public void SetPromptType(InteractableType type)
	{
		switch(type)
		{
			case InteractableType.PICKUP:
				SetPromptType(InteractionIconType.A);
				break;
			case InteractableType.REPAIR:
				SetPromptType(InteractionIconType.B);
				break;
		}
	}
}
