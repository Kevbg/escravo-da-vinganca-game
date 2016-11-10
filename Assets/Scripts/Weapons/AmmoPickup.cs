using UnityEngine;
using System.Collections;

public class AmmoPickup : MonoBehaviour {
	public int ammoType;
    public AudioClip pickupSound;
    private AudioSource sfx;

	// Use this for initialization
	void Start () {
	    sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.CompareTag("Player")) {
			collider.gameObject.GetComponent<Shoot> ().addAmmo (ammoType);
            sfx.PlayOneShot(pickupSound);
			Destroy (transform.parent.gameObject);
		}
	}
}
