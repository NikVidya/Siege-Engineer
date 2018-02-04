using System;
using UnityEngine;

public interface IBombable
{
	void OnBombed(Bombardment instigator);
	float GetDistanceToTransform(Transform t);
}
