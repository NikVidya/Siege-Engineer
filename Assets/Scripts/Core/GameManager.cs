using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject avatar;
    public List<GameObject> activeResources = new List<GameObject>();
    
}
