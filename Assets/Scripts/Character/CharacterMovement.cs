using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [Tooltip("The fastest the player goes under normal movement")]
    public float maxSpeed = 0.05f;
    [Tooltip("Added to the maximum speed when the player is dashing")]
    public float dashAdd = 0.09f;
    [Tooltip("When any input is supplied, player will move at least this fast")]
    public float minSpeed = 0.01f;

    [Header("Movement Feel")]
    [Space(-8)]
    [Header(" - Acceleration")]
    [Tooltip("Defines the shape of the player's acceleration")]
    public AnimationCurve accelerationCurve;
    [Tooltip("Amount of time in seconds that the above curve takes")]
    public float accelerationTime = 0.4f;
    [Space(-8)]
    [Header(" - Dash")]
    [Tooltip("Defines the shape of the dash movement")]
    public AnimationCurve dashCurve;
    [Tooltip("Amount of time in seconds that the above curve takes")]
    public float dashTime = 0.5f;
    [Tooltip("How long it takes until the dash will be available again")]
    public float dashCooldownTime = 0.5f;

    [Header("Encumbrance")]
    [Tooltip("The rampiong of the slowdown created by encumbrance")]
    public AnimationCurve encumbranceCurve;
    [Tooltip("The maximum multiplier for the encumbrance")]
    public float encumbranceMultiplier = 0.5f;

    CapsuleCollider2D _capsule;

    float _timeMoving;
    float _timeDashing;
	private enum MoveState
	{
		READY, MOVING, COOLDOWN
	}
	MoveState _moveState = MoveState.READY;
	MoveState _dashState = MoveState.READY;

    // Use this for initialization
    void Start()
    {
        _capsule = GetComponent<CapsuleCollider2D>();
        GameManager.Instance.avatar = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetButton("Dash"));
    }

    void Move(float horizontal, float vertical, bool shouldDash)
    {
		// Get the desired direction of movement
        Vector2 direction = new Vector2(horizontal, vertical);
        direction.Normalize();

		if (direction.sqrMagnitude > Mathf.Epsilon && _moveState == MoveState.READY) {
			// The player started moving
			_moveState = MoveState.MOVING;
			_timeMoving = 0.0f;
		} else if (direction.sqrMagnitude <= Mathf.Epsilon) {
			// Player is not moving
			_moveState = MoveState.READY;
			return;
		}

		if (shouldDash && _dashState == MoveState.READY) {
			_dashState = MoveState.MOVING;
			_timeDashing = 0.0f;
		}

		// Increment the timers
		_timeMoving += Time.deltaTime;
		_timeDashing += Time.deltaTime;

		// Determine the normal movement velocity
		float vel_normal = Mathf.Clamp(accelerationCurve.Evaluate(_timeMoving / accelerationTime) * maxSpeed, minSpeed, maxSpeed);

		// Determine the Dash velocity
		float vel_dash = 0.0f;
		switch (_dashState) {
		case MoveState.MOVING:
			
			// Compute dash speed
			vel_dash = dashCurve.Evaluate(_timeDashing / dashTime) * dashAdd;

			if (_timeDashing >= dashTime) {
				// Dashed long enough
				_timeMoving = 1.0f; // Force full speed after a dash
				_dashState = MoveState.COOLDOWN;
			}
			break;
		case MoveState.COOLDOWN:
			// TODO Indicate cool down progress

			// Only let the cooldown end if the timer is up AND the player let go of the key
			if (_timeDashing >= dashTime + dashCooldownTime && !shouldDash) {
				// Dash is ready again
				_dashState = MoveState.READY;
			}
			break;
		}

        // Cast the capsule to find where the player will end up
		// Seperate casts so we can slide along walls with diagonal input
		Vector2 dirH = new Vector2(direction.x, 0);
		Vector2 dirV = new Vector2 (0, direction.y);
		RaycastHit2D hitH = Physics2D.CapsuleCast(_capsule.transform.position, _capsule.size, _capsule.direction, 0.0f, dirH, (vel_normal + vel_dash), LayerMask.GetMask(Constants.MOVEMENT_BLOCKING_LAYERS));
		RaycastHit2D hitV = Physics2D.CapsuleCast(_capsule.transform.position, _capsule.size, _capsule.direction, 0.0f, dirV, (vel_normal + vel_dash), LayerMask.GetMask(Constants.MOVEMENT_BLOCKING_LAYERS));

		//Vector3 oldPos = transform.position;

		if (hitH.collider) {
			transform.position = new Vector3(hitH.centroid.x - (direction.x * 0.01f), transform.position.y, transform.position.z); // Place at the hit location, backed off by an amount to account for precision errors
		} else {
			transform.position += new Vector3(direction.x * (vel_normal + vel_dash), 0, 0);
		}

		if (hitV.collider) {
			transform.position = new Vector3(transform.position.x, hitV.centroid.y - (direction.y * 0.01f), transform.position.z); // Place at the hit location, backed off by an amount to account for precision errors
		} else {
			transform.position += new Vector3(0, direction.y * (vel_normal + vel_dash), 0);
		}

		//Debug.DrawLine (oldPos, transform.position, Color.red);
    }
}
