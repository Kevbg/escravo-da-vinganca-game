using UnityEngine;
using System.Collections;

public class Pistol : Weapons {

	// Use this for initialization
	void Start () {
		fireRate = 0.2f;
		lastFire = Time.time;
		reloadTime = 1.2f;
		lastReload = Time.time - reloadTime;
		magazine = 6;
		maxMagazine = 6;
		ammo = 60;
		maxAmmo = 60;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void shoot() {
		if (magazine > 0 && Time.time > fireRate + lastFire && Time.time > reloadTime + lastReload) {
			base.shoot ();
			newBullet.GetComponent<Bullet> ().SetMovement (new LinearMovement ());
			newBullet.GetComponent<Bullet> ().SetDemage (2);
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

	public override void addAmmo() {
		base.addAmmo ();
		if (ammo + 12 > maxAmmo) {
			ammo = maxAmmo;
		} else {
			ammo += 12;
		}
	}
}