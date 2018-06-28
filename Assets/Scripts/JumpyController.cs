using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyController : PhysicsObject
{
	// Use this for initialization
	void Start()
	{

	}

	protected override void ComputeMovement()
	{
		targetVelocity.x = Input.GetAxis("Horizontal");
		targetVelocity.y = Input.GetAxis("Vertical");

	}
}
