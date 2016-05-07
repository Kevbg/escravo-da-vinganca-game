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

		if (Input.GetAxisRaw("Jump") > 0) {
			owner.currentState = new JumpingState (owner);
		}

		if (!owner.isGrounded) {
			owner.currentState = new FallingState (owner);
		}

		owner.move ();
	}
}
