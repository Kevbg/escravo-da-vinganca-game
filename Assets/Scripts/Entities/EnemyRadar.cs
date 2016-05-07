using UnityEngine;
using System.Collections;

public class EnemyRadar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("Player")) {
			this.GetComponentInParent<Shoot> ().shoot ();
		}
	}
}
