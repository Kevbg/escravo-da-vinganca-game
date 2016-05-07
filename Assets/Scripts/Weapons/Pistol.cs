using UnityEngine;
using System.Collections;

public class Pistol : Weapons {

	// Use this for initialization
	void Start () {
		fireRate = 0.1f;
		lastFire = Time.time;
		reloadTime = 1.0f;
		lastReload = Time.time - reloadTime;
		magazine = 10;
		maxMagazine = 10;
		ammo = 100;
		maxAmmo = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void shoot() {
		if (magazine > 0 && Time.time > fireRate + lastFire && Time.time > reloadTime + lastReload) {
			base.shoot ();
			newBullet.GetComponent<Bullet> ().SetMovement (new LinearMovement ());
			newBullet.GetComponent<Bullet> ().SetDemage (3);
		} else if (magazine == 0) {
			this.reload ();
		} else {

		}
	}

	public override void reload() {
		base.reload ();
	}

	public override void selectWeapon() {
		base.selectWeapon ();
	}
}