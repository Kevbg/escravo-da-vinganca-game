using UnityEngine;
using System.Collections;

public class LaserGun : IWeapon {
	public GameObject owner;

	public float fireDelay;
	public float timeSinceLastShoot;

	public LaserGun (GameObject owner, float fireDelay) {
		this.owner = owner;
		this.fireDelay = fireDelay;
	}

	public void shoot() {
		if (Time.time > fireDelay + timeSinceLastShoot) {
			timeSinceLastShoot = Time.time;

			Debug.Log ("Shooted with laser...");
		} else {
			Debug.Log ("Too early to shoot with this weapon...");
		}

	}

	public void onSelectedWeapon() {
		
	}
}