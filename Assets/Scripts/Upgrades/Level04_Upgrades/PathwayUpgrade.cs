﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathwayUpgrade : BaseUpgrade {

	protected override void OnApplyUpgrade (GameObject playerObject)
	{
		Destroy(GameObject.Find("Pathway"));
	} 

	protected override void OnRemoveUpgrade (GameObject playerObject)
	{

	}

}
