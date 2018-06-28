using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float GravityModifier = 1f;
	public int MaxVelocity = 10;
	protected Vector3 targetVelocity;
	protected Vector3 jumpVelocity;
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

	public bool IsGrounded()
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

		rb.AddForce(Physics.gravity * GravityModifier);

		Debug.Log(rb.velocity.x);

		if (Mathf.Abs(rb.velocity.x) <= MaxVelocity)
		{
			if (rb.velocity.x <= MaxVelocity / 2)
			{
				rb.AddForce(new Vector3(targetVelocity.x * 3, 0f, 0f));
			}
			else
			{
				rb.AddForce(new Vector3(targetVelocity.x, 0f, 0f));
			}
		}
		rb.AddForce(new Vector3(0f, targetVelocity.y, 0f), ForceMode.Impulse);
	}
}
