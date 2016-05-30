using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour {
	public int ammoType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.CompareTag("Player")) {
			collider.gameObject.GetComponent<Shoot> ().addAmmo (ammoType);
			Destroy (gameObject);
		}
	}
}
