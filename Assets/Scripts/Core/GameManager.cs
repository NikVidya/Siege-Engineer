using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject avatar;
    public List<CharacterInventory.IHoldable> holdables = new List<CharacterInventory.IHoldable>();
}
