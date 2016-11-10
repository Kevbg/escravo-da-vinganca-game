using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entities : MonoBehaviour {
	public Transform spawWeapon;

	public IState currentState;

	public bool isGrounded;

	public float health;
    public bool invulnerable;
	//public bool invulnerable;
	//public float invulnerableTime;
 //   public bool canBeInvulnerable;

	// Use this for initialization
	public virtual void onStart () {
		//invulnerable = false;
		//invulnerableTime = 0;
  //      canBeInvulnerable = true;
	}
	
	// Update is called once per frame
	public virtual void onUpdate () {
		//if (canBeInvulnerable && invulnerable) {
		//	this.GetComponent<SpriteRenderer> ().enabled = !this.GetComponent<SpriteRenderer> ().enabled;
		//	invulnerableTime += Time.deltaTime;
		//}
		//if (invulnerableTime > 1) {
		//	invulnerableTime = 0;
		//	invulnerable = false;
		//	this.GetComponent<SpriteRenderer> ().enabled = true;
		//}
		currentState.Update ();
	}

    public IEnumerator blink(int count) {
        float blinkInterval = 0.02f;

        for(int i = 0; i < count; i++) {
            if (health > 0) {
                GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(blinkInterval);
                GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(blinkInterval);
            }
        }

        invulnerable = false;
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
