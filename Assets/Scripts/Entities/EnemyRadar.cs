using UnityEngine;
using System.Collections;

public class EnemyRadar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag ("Player")) {
			Transform transform = this.gameObject.transform.parent;
			Transform player = collider.transform;

			if (transform.position.x > player.position.x && transform.localScale.x > 0) {
				Vector3 scale = this.gameObject.transform.parent.localScale;
				scale.x *= -1;
				this.gameObject.transform.parent.localScale = scale;
			} else if (transform.position.x < player.position.x && transform.localScale.x < 0) {
				Vector3 scale = this.gameObject.transform.parent.localScale;
				scale.x *= -1;
				this.gameObject.transform.parent.localScale = scale;
			}
			this.gameObject.GetComponentInParent<Enemy> ().isFacingPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		this.gameObject.GetComponentInParent<Enemy> ().isFacingPlayer = false;
	}
}
