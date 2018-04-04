﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenPathway : BaseUpgrade {

	protected override void OnApplyUpgrade (GameObject playerObject)
	{
		Destroy(GameObject.Find("HiddenPath"));
	} 

	protected override void OnRemoveUpgrade (GameObject playerObject)
	{

	}

}
