using UnityEngine;
using System.Collections;

public class IdleState : IState {
	Entities owner;

	public IdleState(Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Idle");
	}
	
	// Update is called once per frame
	public void Update () {
        if (!MenuController.gamePaused) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                owner.currentState = new RunningState(owner);
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                owner.currentState = new JumpingState(owner);
            }

            if (!owner.isGrounded) {
                owner.currentState = new FallingState(owner);
            }
        }

	}
}
