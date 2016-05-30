using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entities : MonoBehaviour {
	public Transform spawWeapon;

	public IState currentState;

	public bool isGrounded;

	public float health;
	public float invulnerableTime;
	public bool invulnerable;

	// Use this for initialization
	public virtual void onStart () {
		invulnerable = false;
		invulnerableTime = 0;
	}
	
	// Update is called once per frame
	public virtual void onUpdate () {
		if (invulnerable) {
			this.GetComponent<SpriteRenderer> ().enabled = !this.GetComponent<SpriteRenderer> ().enabled;
			invulnerableTime += Time.deltaTime;
		}
		if (invulnerableTime > 1) {
			invulnerableTime = 0;
			invulnerable = false;
			this.GetComponent<SpriteRenderer> ().enabled = true;
		}
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
