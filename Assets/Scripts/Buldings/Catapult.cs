using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Damageable))]
public class Catapult : MonoBehaviour
{
	[Tooltip("Percentage of damage blocked when this component is alive component")]
	public float damageDebuf = 0.01f;

	bool isAlive = true;
	Damageable damageableComponent;

	// Use this for initialization
	void Start ()
	{
		damageableComponent = GetComponent<Damageable> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (damageableComponent.health <= 0.0f && isAlive) {
			GameManager.Instance.RemoveDamageDeBuff (this);
			isAlive = false;
		} else if (damageableComponent.health > 0.0f && !isAlive) {
			isAlive = true;
			GameManager.Instance.AddDamageDeBuff (this);
		}
	}




}

