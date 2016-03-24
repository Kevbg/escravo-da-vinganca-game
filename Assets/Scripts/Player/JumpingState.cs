using UnityEngine;
using System.Collections;

public class JumpingState : IState {
	Entities owner;

	float xAxis;

	public JumpingState (Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		owner.GetComponent<Rigidbody2D>().AddForce(owner.transform.up * 900);
		Debug.Log ("Jumping...");
	}
	
	// Update is called once per frame
	public void Update () {
		xAxis = Input.GetAxisRaw ("Horizontal");
		move ();

		if (owner.GetComponent<Rigidbody2D> ().velocity.y <= 0) {
			owner.currentState = new FallingState (owner);
		}
	}

	void move(float speed = 30) {
		Vector2 newPosition = new Vector2(owner.transform.position.x + xAxis, owner.transform.position.y);
		owner.transform.position = Vector2.Lerp(owner.transform.position, newPosition, Time.deltaTime * speed);
	}
}
