﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenPathway : BaseUpgrade {

	protected override void OnApplyUpgrade (GameObject playerObject)
	{
		Destroy(GameObject.Find("removableLedge"));
	} 

	protected override void OnRemoveUpgrade (GameObject playerObject)
	{
		
	}
		
}
