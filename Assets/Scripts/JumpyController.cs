using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyController : MonoBehaviour
{
	//Movement speed of character
	public int Acceleration;
	//Jump power of character
	public int JumpPower;
	public int MaxVelocity;

	//Movement state of character (can only be one)
	private enum PlayerState { Grounded, Idle, Airborne, Stunned, Crouching };

	//Possible movement inputs
	private enum MovementInput { Left, Right, Jump, Crouch, Idle };

	//Values associated with movement input
	private Dictionary<MovementInput, int> MovementVal = new Dictionary<MovementInput, int>{
		{MovementInput.Left, -1},
		{MovementInput.Right, 1},
		{MovementInput.Crouch, -1},
		{MovementInput.Jump, 1},
		{MovementInput.Idle, 0},
	};

	private Rigidbody rb;
	private PlayerState moveState;


	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		moveState = PlayerState.Airborne;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		Movement(HorizontalMovement(), VerticalMovement());
		Debug.Log(moveState);
	}

	/**
	 * Perform movement operation on object
	 * horizontal 	= -1, 0, 1 based on whether left, none, or right is being held down
	 * vertical 	= -1, 0, 1 based on whether up, none, or down is being held down
	 */
	private void Movement(int horizontal, int vertical)
	{
		if (horizontal == 0 && moveState == PlayerState.Grounded)
		{
			rb.velocity = new Vector3(0, rb.velocity.y, 0);
		}
		if (rb.velocity == Vector3.zero)
		{
			moveState = PlayerState.Grounded;
		}
		//Idle case
		if (horizontal == 0 && vertical == 0)
		{
			return;
		}


		float moveSpeed = Acceleration;
		float tempMaxVel = MaxVelocity;

		if (moveState == PlayerState.Airborne)
		{
			moveSpeed /= 2;
		}
		//Scale speed if shift is held down
		if (Input.GetKey(KeyCode.LeftShift) && horizontal != 0)
		{
			tempMaxVel *= 2f;
		}
		//Make sure character is not travelling too fast
		if (Math.Abs(rb.velocity.x) < tempMaxVel)
		{
			//Update horizontal force to character
			rb.AddForce(new Vector3(horizontal * moveSpeed, 0, 0));
		}


		//Determine and apply vertical input
		if (vertical == 1 && moveState != PlayerState.Airborne && Math.Abs(rb.velocity.y) < tempMaxVel)
		{
			Jump();
		}
		else if (vertical == -1)
		{
			Crouch();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		moveState = PlayerState.Grounded;
	}

	void OnCollisionExit(Collision other)
	{
		moveState = PlayerState.Airborne;
	}

	private void Jump()
	{
		rb.AddForce(new Vector3(0, 1 * JumpPower, 0));
		moveState = PlayerState.Airborne;
	}

	//Purely vertical boost
	private void Crouch()
	{

	}

	/* Determines what vertical input is being recieved
	 * Returns 1 if up is being held
	 * Returns 0 if neither up or down is being held
	 * Returns -1 if down is being held
	 */
	private int VerticalMovement()
	{
		float AxisVertical = Input.GetAxis("Vertical");
		if (AxisVertical > 0 || Input.GetKey(KeyCode.Space))
		{
			return MovementVal[MovementInput.Jump];
		}
		else if (AxisVertical < 0)
		{
			return MovementVal[MovementInput.Crouch];
		}
		return MovementVal[MovementInput.Idle];
	}

	/* Determines what vertical input is being recieved
	 * Returns 1 if right is being held
	 * Returns 0 if neither right or left is being held
	 * Returns -1 if left is being held
	 */
	private int HorizontalMovement()
	{
		float AxisHorizontal = Input.GetAxis("Horizontal");

		if (AxisHorizontal < 0)
		{
			return MovementVal[MovementInput.Left];
		}
		else if (AxisHorizontal > 0)
		{
			return MovementVal[MovementInput.Right];
		}
		return MovementVal[MovementInput.Idle];
	}
}
