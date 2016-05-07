using UnityEngine;
using System.Collections;

public class FallingState : IState {
	Entities owner;

	float xAxis;

	public FallingState (Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Falling...");
	}
	
	// Update is called once per frame
	public void Update () {
		if (owner.isGrounded) {
			owner.currentState = new IdleState (owner);
		}

		xAxis = Input.GetAxisRaw ("Horizontal");
		owner.move ();
	}

	void move(float speed = 30) {
		Vector2 newPosition = new Vector2(owner.transform.position.x + xAxis, owner.transform.position.y);
		owner.transform.position = Vector2.Lerp(owner.transform.position, newPosition, Time.deltaTime * speed);
	}
}
