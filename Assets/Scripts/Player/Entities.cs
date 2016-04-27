using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entities : MonoBehaviour {
	public Transform spawWeapon;

	public IState currentState;

	public bool isGrounded;

	// Use this for initialization
	public virtual void onStart () {
		
	}
	
	// Update is called once per frame
	public virtual void onUpdate () {
		currentState.Update ();
	}

	public virtual void move (float speed = 30) {
	
	}
}
