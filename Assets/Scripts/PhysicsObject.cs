using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float gravityModifier = 1f;

	protected Vector3 velocity;
	protected Rigidbody rb;
	protected ContactFilter2D contactFilter;
	protected const float minMoveDistance = 0.001f;

	void OnEnable()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		velocity += gravityModifier * Physics.gravity * Time.deltaTime;

		Vector3 deltaPos = velocity * Time.deltaTime;

		Vector3 move = Vector3.up * deltaPos.y;

		Movement(move);
	}

	void Movement(Vector3 move)
	{
		float distance = move.magnitude;

		if (distance > minMoveDistance)
		{

		}

		rb.position = rb.position + move;
	}
}
