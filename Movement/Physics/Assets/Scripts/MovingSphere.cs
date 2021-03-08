using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{

	[SerializeField, Range(0f, 100f)]
	float maxSpeed = 10f;

	[SerializeField, Range(0f, 100f)]
	float maxAcceleration = 10f;

	[SerializeField, Range(0f, 10f)]
	float jumpHeight = 2f;

	[SerializeField, Range(0, 5)]
	int maxAirJump = 0;

	int jumpPhase;

	Vector3 velocity, desiredVelocity;

	Rigidbody body;

	bool desiredJump;
	bool onGround;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);

		desiredVelocity =
			new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
		desiredJump |= Input.GetButtonDown("Jump");
	}

    private void FixedUpdate()
    {
		UpdateState();
		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		velocity.x =
			Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
		velocity.z =
			Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        if (desiredJump)
        {
			desiredJump = false;
			Jump();
        }
		body.velocity = velocity;
		onGround=false;
	}

	void Jump()
    {
		if (onGround)
		{
			velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
		}
    }

    private void OnCollisionEnter(Collision collision)
    {
		EvaluateCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
		EvaluateCollision(collision);
    }

	void EvaluateCollision(Collision collision)
    {
		for(int i = 0; i < collision.contactCount; i++)
        {
			Vector3 normal = collision.GetContact(i).normal;
			onGround |= normal.y >= 0.9f;
        }
    }

	void UpdateState()
    {
		velocity = body.velocity;
        if (onGround)
        {
			jumpPhase = 0;
        }
	}
}