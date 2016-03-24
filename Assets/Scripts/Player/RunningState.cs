using UnityEngine;
using System.Collections;

public class RunningState : IState {
	Entities owner;

	float xAxis;

	public RunningState (Entities owner){
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Running");
	}
	
	// Update is called once per frame
	public void Update () {
		xAxis = Input.GetAxisRaw ("Horizontal");

		if (xAxis == 0) {
			owner.currentState = new IdleState (owner);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			owner.currentState = new JumpingState (owner);
		}

		if (!owner.isGrounded) {
			owner.currentState = new FallingState (owner);
		}

		move ();
	}

	void move(float speed = 30) {
		Vector2 newPosition = new Vector2(owner.transform.position.x + xAxis, owner.transform.position.y);
		owner.transform.position = Vector2.Lerp(owner.transform.position, newPosition, Time.deltaTime * speed);
	}
}
