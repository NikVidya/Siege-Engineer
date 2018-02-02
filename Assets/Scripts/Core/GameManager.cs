using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject avatar;
    public List<CharacterInventory.IHoldable> holdables = new List<CharacterInventory.IHoldable>();

	public List<Damageable> damagebales = new List<Damageable>();

	public void RegisterDamageable(Damageable damageable)
	{
		defensiveStructures.Add (damageable);
	}

	public void DeRegisterDamageable(Damageable damageable)
	{
		defensiveStructures.Remove (damageable);
	}

	public void RemoveDamageDeBuff(Catapult defensiveStructure)
	{
		foreach (Damageable damageable in damagebales) {
			damageable.damageDeBuff -= defensiveStructure.damageDebuff;
		}
	}

	public void AddDamageDeBuff(Catapult defensiveStructure)
	{
		foreach (Damageable damageable in damageables) {
			damageable.damageDeBuff += defensiveStructure.damageDebuff;
		}
	}
}
