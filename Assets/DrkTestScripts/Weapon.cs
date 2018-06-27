using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject Bullet;

	private PlayerMovement playerMovement;


	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement>();

		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)) {
			var tBullet = Instantiate(Bullet, gameObject.transform.position, Bullet.transform.rotation) as GameObject;
			tBullet.GetComponent<Bullet>().bulletDirection = playerMovement.PlayerDirection;
		}
		
	}
}
