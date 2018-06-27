using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {LEFT, RIGHT};
public class PlayerMovement : MonoBehaviour {

	public float speed = 5.0f;
	public bool isOnGround = false;
	public float jumpPower = 7.0f;

	private Transform _transform;
	private Rigidbody _rigidbody;
	private Direction playerDirection = Direction.RIGHT;

	public Direction PlayerDirection {
		get {
			return playerDirection;
		}
	}

	// Use this for initialization
	void Start () {
		_transform = GetComponent(typeof (Transform)) as Transform;
		_rigidbody = GetComponent(typeof (Rigidbody)) as Rigidbody;

		
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer();
	    Jump();
		
	}

	void MovePlayer() {
		float translate = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.Translate(translate, 0, 0);

		if(translate > 0) {
			playerDirection = Direction.RIGHT;
		} else if (translate < 0) {
			playerDirection = Direction.LEFT;
		}
	}

	void Jump() {
		if(Input.GetKeyDown(KeyCode.W) && isOnGround) {
			_rigidbody.AddForce(new Vector2(0, jumpPower), ForceMode.Impulse);
		}

	}

	void OnCollisionEnter() {
		isOnGround = true;
	
	}

	void OnCollisionExit() {
		isOnGround = false;
	}
}
