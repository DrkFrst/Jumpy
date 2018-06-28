using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyController : PhysicsObject
{
	public int TakeOffSpeed;
	public int Speed;
	// Use this for initialization
	void Start()
	{

	}

	bool IsJumping()
	{
		if (Input.GetAxis("Vertical") > 0 || Input.GetKey(KeyCode.Space))
		{
			return true;
		}
		return false;
	}

	protected override void ComputeMovement()
	{
		targetVelocity = Vector3.zero;
		targetVelocity.x = Input.GetAxis("Horizontal") * Speed;

		if (IsGrounded() && IsJumping())
		{
			targetVelocity.y = TakeOffSpeed;
		}
		Debug.Log(targetVelocity);
	}
}
