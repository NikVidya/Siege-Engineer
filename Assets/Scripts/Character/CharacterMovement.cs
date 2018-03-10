using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [Tooltip("The fastest the player goes under normal movement")]
    public float maxSpeed = 0.05f;
    [Tooltip("Added to the maximum speed when the player is dashing")]
    public float dashAdd = 0.09f;

    [Header("Movement Feel")]
    [Space(-8)]
    [Header(" - Dash")]
    [Tooltip("Defines the shape of the dash movement")]
    public AnimationCurve dashCurve;
    [Tooltip("Amount of time in seconds that the above curve takes")]
    public float dashTime = 0.5f;
    [Tooltip("How long it takes until the dash will be available again")]
    public float dashCooldownTime = 0.5f;

    [Header("Encumbrance")]
    [Tooltip("The number of items help to max encumbrance")]
    public int encumbranceMaxCount = 3;
    [Tooltip("The ramping of the slowdown created by encumbrance")]
    public AnimationCurve encumbranceCurve;
    [Tooltip("The maximum multiplier for the encumbrance")]
    public float encumbranceMultiplier = 0.5f;

    [Header("Stun")]
    [Tooltip("The amount of time the player should be stunned for")]
    public float stunDuration = 1.0f;

    [Header("Visuals")]
    [Tooltip("The animator component which controls the sprite animation")]
    public Animator animator;

    [HideInInspector]
    public bool paused = false;
    private const string ANIMATOR_SPEED_TAG = "speed";
    private const string ANIMATOR_DIRECTION_TAG = "direction";

    CapsuleCollider2D _capsule;
    CharacterInventory _inventory;

    float _timeMoving;
    float _timeDashing;
    private enum MoveState
    {
        Ready, Moving, Cooldown
    }
    MoveState _moveState = MoveState.Ready;
    MoveState _dashState = MoveState.Ready;

    float _timeStunned;

    bool movementEnabled = true;

    public void DisableMovement()
    {
        movementEnabled = false;
    }
    public void EnableMovement()
    {
        movementEnabled = true;
    }

    // Use this for initialization
    void Start()
    {
        _capsule = GetComponent<CapsuleCollider2D>();
        _inventory = GetComponent<CharacterInventory>();
        _timeStunned = stunDuration;
    }

    // Update is called once per frame
    void Update()
	{
		if (_timeStunned <= stunDuration) {
			_timeStunned += Time.deltaTime;
		}
	}

	void LateUpdate()
	{
		if (!paused && _timeStunned > stunDuration)
        {
            Move(Input.GetAxisRaw(Constants.InputNames.HORIZONTAL), Input.GetAxisRaw(Constants.InputNames.VERTICAL), Input.GetButton(Constants.InputNames.DASH));
        }
    }

    public void Stun()
    {
        _timeStunned = 0;
    }

    void SetAnimatorDirection(Vector2 dir)
    {
        int dirVal = 6;

        if(dir.y > Mathf.Epsilon)
        {   // Moving North
            if(dir.x < -Mathf.Epsilon)
            {   // Moving West
                dirVal = 1;
            }
            else if(dir.x > Mathf.Epsilon)
            {   // Moving East
                dirVal = 3;
            }
            else
            {   // Not East or West
                dirVal = 2;
            }
        }
        else if(dir.y < -Mathf.Epsilon)
        {   // Moving South
            if(dir.x > Mathf.Epsilon)
            {   // Moving East
                dirVal = 5;
            }
            else if(dir.x < -Mathf.Epsilon)
            {   // Moving West
                dirVal = 7;
            }
            else
            {
                dirVal = 6;
            }
        }
        else
        {   // Not South or North
            if(dir.x < -Mathf.Epsilon)
            {   // Moving West
                dirVal = 0;
            }
            else if(dir.x > Mathf.Epsilon)
            {   // Moving East
                dirVal = 4;
            }
        }


        /*
        if(dir.x > Mathf.Epsilon)
        { // Has east movement
            if(dir.y < Mathf.Epsilon)
            {   // Has south movement
                dirVal = 5;
            }
            else if(dir.y > Mathf.Epsilon)
            {   // Has north movement
                dirVal = 3;
            }
            else
            {   // Has no vertical movement
                dirVal = 4;
            }
        }
        else if(dir.x < Mathf.Epsilon)
        { // Has west movement
            if (dir.y < Mathf.Epsilon)
            {   // Has south movement
                dirVal = 7;
            }
            else if (dir.y > Mathf.Epsilon)
            {   // Has north movement
                dirVal = 1;
            }
            else
            {   // Has no vertical movement
                dirVal = 0;
            }

        }
        else
        { // Has no horizontal movement
            if (dir.y < Mathf.Epsilon)
            {   // Has south movement
                dirVal = 6;
            }
            else if (dir.y > Mathf.Epsilon)
            {   // Has north movement
                dirVal = 2;
            }
            else
            {   // Has no vertical movement
                dirVal = 0;
            }
        }
        */

        animator.SetInteger(ANIMATOR_DIRECTION_TAG, dirVal);
    }

    void Move(float horizontal, float vertical, bool shouldDash)
    {
        // Get the desired direction of movement
        Vector2 direction = new Vector2(horizontal, vertical);
        direction.Normalize();

        SetAnimatorDirection(direction);

        if (direction.sqrMagnitude > Mathf.Epsilon && _moveState == MoveState.Ready && movementEnabled)
        {
            // The player started moving
            _moveState = MoveState.Moving;
            _timeMoving = 0.0f;
        }
        else if (direction.sqrMagnitude <= Mathf.Epsilon || !movementEnabled)
        {
            // Player is not moving
            _moveState = MoveState.Ready;
            animator.SetFloat(ANIMATOR_SPEED_TAG, 0.0f);
            return;
        }

        if (shouldDash && _dashState == MoveState.Ready)
        {
            _dashState = MoveState.Moving;
            _timeDashing = 0.0f;
        }

        // Increment the timers
        _timeMoving += Time.deltaTime;
        _timeDashing += Time.deltaTime;

        // Determine the normal movement velocity
        float vel_normal = maxSpeed;

        // Determine the Dash velocity
        float vel_dash = 0.0f;
        switch (_dashState)
        {
            case MoveState.Moving:

                // Compute dash speed
                vel_dash = dashCurve.Evaluate(_timeDashing / dashTime) * dashAdd;

                if (_timeDashing >= dashTime)
                {
                    // Dashed long enough
                    _timeMoving = 1.0f; // Force full speed after a dash
                    _dashState = MoveState.Cooldown;
                }
                break;
            case MoveState.Cooldown:
                // TODO Indicate cool down progress

                // Only let the cooldown end if the timer is up AND the player let go of the key
                if (_timeDashing >= dashTime + dashCooldownTime && !shouldDash)
                {
                    // Dash is ready again
                    _dashState = MoveState.Ready;
                }
                break;
        }
        float curEncumbranceMultiplier = 1 - encumbranceMultiplier * encumbranceCurve.Evaluate(_inventory.NumHeldItems / (float)encumbranceMaxCount);
        float total_vel = (vel_normal + vel_dash) * curEncumbranceMultiplier;

        animator.SetFloat(ANIMATOR_SPEED_TAG, total_vel);

        // Cast the capsule to find where the player will end up
        // Seperate casts so we can slide along walls with diagonal input
        Vector2 dirH = new Vector2(direction.x, 0);
        Vector2 dirV = new Vector2(0, direction.y);
        RaycastHit2D hitH = Physics2D.CapsuleCast(_capsule.transform.position, _capsule.size, _capsule.direction, 0.0f, dirH, total_vel, LayerMask.GetMask(Constants.MOVEMENT_BLOCKING_LAYERS));
        RaycastHit2D hitV = Physics2D.CapsuleCast(_capsule.transform.position, _capsule.size, _capsule.direction, 0.0f, dirV, total_vel, LayerMask.GetMask(Constants.MOVEMENT_BLOCKING_LAYERS));

        //Vector3 oldPos = transform.position;

        if (hitH.collider)
        {
			transform.position = new Vector3(hitH.centroid.x - (direction.x * 0.01f), transform.position.y, transform.position.z); // Place at the hit location, backed off by an amount to account for precision errors
        }
        else
        {
            transform.position += new Vector3(direction.x * total_vel, 0, 0);
        }

        if (hitV.collider)
        {
			transform.position = new Vector3(transform.position.x, hitV.centroid.y - (direction.y * 0.01f), transform.position.z); // Place at the hit location, backed off by an amount to account for precision errors
        }
        else
        {
            transform.position += new Vector3(0, direction.y * total_vel, 0);
        }

        //Debug.DrawLine (oldPos, transform.position, Color.red);
    }
}
