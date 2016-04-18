using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entities {
	public float moveSpeed;

	public const float DefaultSpeed = 30;
	private static float xAxis;

	void Start () {
		base.onStart ();
		moveSpeed = DefaultSpeed;

		currentState = new IdleState (this);
	}

	void Update () {
		base.onUpdate ();

		if (Input.GetAxisRaw("Fire1") > 0) {
			weaponsController.GetComponent<WeaponsController> ().shoot ();
		}
		if (Input.GetAxisRaw("Reload1") > 0) {
			weaponsController.GetComponent<WeaponsController> ().reload ();
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			weaponsController.GetComponent<WeaponsController> ().changeWeapon (-1);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			weaponsController.GetComponent<WeaponsController> ().changeWeapon (1);
		}

		xAxis = Input.GetAxisRaw ("Horizontal");
		if (getDirection () > 0) {
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (getDirection () < 0) {
			transform.localRotation = Quaternion.Euler (0, 180, 0);
		}
	}

	public static float getDirection () {
		return xAxis;
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.name == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.name == "Ground") {
			isGrounded = false;
		}
	}

	public override void move (float speed = 30) {
		Vector2 newPosition = new Vector2(transform.position.x + xAxis, transform.position.y);
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * speed);
	}
}
