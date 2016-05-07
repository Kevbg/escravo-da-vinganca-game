using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entities {
	public float moveSpeed;
	public float vely;
	public const float DefaultSpeed = 30;
	private static float xAxis;

	void Start () {
		base.onStart ();
		moveSpeed = DefaultSpeed;
		health = 5;

		currentState = new IdleState (this);
	}

	void Update () {
		base.onUpdate ();
		vely = GetComponent<Rigidbody2D> ().velocity.y;

		if (Input.GetAxisRaw("Fire1") > 0) {
			this.GetComponent<Shoot> ().shoot ();
		}
		if (Input.GetAxisRaw("Reload1") > 0) {
			this.GetComponent<Shoot> ().reload ();
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			this.GetComponent<Shoot> ().changeWeapon (-1);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			this.GetComponent<Shoot> ().changeWeapon (1);
		}

		xAxis = Input.GetAxisRaw ("Horizontal");
		if (xAxis > 0 && this.transform.localScale.x < 0) {
			Vector3 scale = this.transform.localScale;
			scale.x *= -1;
			this.transform.localScale = scale;
		} else if (xAxis < 0 && this.transform.localScale.x > 0) {
			Vector3 scale = this.transform.localScale;
			scale.x *= -1;
			this.transform.localScale = scale;
		}
	}

	void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	public override void move (float speed = 30) {
		Vector2 newPosition = new Vector2(transform.position.x + xAxis, transform.position.y);
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * speed);
	}
}
