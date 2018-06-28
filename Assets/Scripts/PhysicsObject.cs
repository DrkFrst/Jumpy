using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float gravityModifier = 1f;

	protected Vector3 targetVelocity;
	protected Vector3 velocity;
	protected Rigidbody rb;

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
		ComputeMovement();
	}

	void FixedUpdate()
	{
		Movement();
	}

	bool IsGrounded()
	{
		Vector3 down = transform.TransformDirection(Vector3.down);
		float size = transform.GetComponent<Collider>().bounds.extents.y;
		if (Physics.Raycast(transform.position, down, size))
		{
			return true;
		}
		return false;
	}

	protected virtual void ComputeMovement()
	{

	}

	public void Movement()
	{
		rb.AddForce(Physics.gravity * gravityModifier);
		rb.AddForce(targetVelocity, ForceMode.Impulse);
	}
}
