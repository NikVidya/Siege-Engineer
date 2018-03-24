using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardment : MonoBehaviour {

	[Header("Bombardement Parameters")]
	[Tooltip("The amount of time the bombardment will follow the player")]
	public float moveTime = 5.0f;
	[Tooltip("The amount of time after movement ends before the bombardment happens")]
	public float hitDelay = 1.0f;
	[Tooltip("The rate at which the target moves towards the player")]
	public float moveRate = 2.0f;
	[Tooltip("The size of the bombardment area")]
	public float bombardmentArea = 1.5f;
	[Tooltip("The amount of damage to deal to all objects within the bombardment range")]
	public int damage = 10;

	float timeAlive = 0.0f;
    bool canExplode = true;

    const string ANIMATOR_DETONATE_TAG = "detonate";
    Animator animator;

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(255f,0f,0f,0.3f);
		Gizmos.DrawSphere (transform.position, bombardmentArea);
	}

	// Use this for initialization
	void Start () {
		timeAlive = 0.0f;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (timeAlive <= moveTime) {
			Vector3 movement = Vector3.ClampMagnitude(GameManager.Instance.interactionComponent.transform.position - transform.position, moveRate * Time.deltaTime);

			transform.position = transform.position + movement;
		} else if (timeAlive > moveTime + hitDelay && canExplode) {
            canExplode = false;
            List<IBombable> bombables = GameManager.Instance.GetBombablesInRange (transform, bombardmentArea);
			foreach (IBombable bombable in bombables) {
				bombable.OnBombed (this);
			}
            if(animator != null)
            {
                animator.SetTrigger(ANIMATOR_DETONATE_TAG);
            }
		}
		timeAlive += Time.deltaTime;
	}

    public void EndDetonateAnim()
    {
        Destroy(gameObject); // Commit suicide
    }
}
