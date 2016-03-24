using UnityEngine;
using System.Collections;

public class Pistol : IWeapon {
	public GameObject owner;

	public float fireDelay;
	public float timeSinceLastShoot;

	public Pistol (GameObject owner, float fireDelay) {
		this.owner = owner;
		this.fireDelay = fireDelay;
	}

	public void shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			timeSinceLastShoot = Time.time;

			Debug.Log ("Shooted with pistol...");
		} else {
			Debug.Log ("Too early to shoot with this weapon...");
		}

	}

	public Color onSelectedWeapon() {
		return Color.yellow;
	}
}
