using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Entities {
	public float moveSpeed;
	public float vely;
	public const float DefaultSpeed = 30;
	private static float xAxis;

	public GameObject enemyRadar;
	public bool isFacingPlayer = false;

	void Start () {
		base.onStart ();
		moveSpeed = DefaultSpeed;
		health = Random.Range(6, 8);

		currentState = new EnemyIdleState (this);
	}

	void Update () {
		base.onUpdate ();
		vely = GetComponent<Rigidbody2D> ().velocity.y;

		if (isFacingPlayer) {
			currentState = new EnemyAlertState (this);
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
		Vector2 newPosition = new Vector2(transform.position.x + this.getDirection(), transform.position.y);
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * speed);
	}

	public override void noHealth ()
	{
		base.noHealth ();

		float chance = Random.Range (0f, 1f);
		if (chance > 0.9f) {
			WeaponsController.weaponsController.CreatePickupAmmo (1, this.transform);
		}else if (chance > 0.7f) {
			WeaponsController.weaponsController.CreatePickupAmmo (0, this.transform);
		}

		Destroy (this.gameObject);
	}
}