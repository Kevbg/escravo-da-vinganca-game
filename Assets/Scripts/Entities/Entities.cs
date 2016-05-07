using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entities : MonoBehaviour {
	public Transform spawWeapon;

	public IState currentState;

	public bool isGrounded;

	public float health;

	// Use this for initialization
	public virtual void onStart () {
		
	}
	
	// Update is called once per frame
	public virtual void onUpdate () {
		currentState.Update ();
	}

	public virtual void move (float speed = 30) {
	
	}

	public virtual void noHealth() {
		
	}

	public virtual int getDirection() {
		if (this.transform.localScale.x > 0) {
			return 1;
		} else {
			return -1;
		}
	}
}
