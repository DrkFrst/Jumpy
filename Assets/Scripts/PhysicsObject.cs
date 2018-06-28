using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float gravityModifier = 1f;

	private bool isGrounded;

	protected Vector3 targetVelocity;
	protected Vector3 velocity;
	protected Rigidbody rb;
	protected const float minMoveDistance = 0.001f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
	}

	void FixedUpdate()
	{
		velocity.x = targetVelocity.x;

		Vector3 moveOnGround = new Vector3();

		Vector3 deltaPos = velocity * Time.deltaTime;

		Vector3 move = Vector3.up * deltaPos.y;

		Movement(move);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Ground")
		{
			isGrounded = false;
		}
	}

	public void Movement(Vector3 move)
	{
		rb.AddForce(Physics.gravity * gravityModifier);

	}
}
