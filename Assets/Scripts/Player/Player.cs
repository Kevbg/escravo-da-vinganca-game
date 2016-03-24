using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entities {
	public float moveSpeed;

	private const float DefaultSpeed = 30;
	private static float xAxis;

	void Start () {
		moveSpeed = DefaultSpeed;

		currentState = new IdleState (this);
		weapon.transform.position = this.transform.position;
	}

	void Update () {
		currentState.Update ();
		weapon.transform.position = this.transform.position;
	}

	public static float getDirection() {
		return xAxis;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.name == "Ground") {
			isGrounded = false;
		}
	}
}
