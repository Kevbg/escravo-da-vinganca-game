using UnityEngine;
using System.Collections;

public class Pistol1 : Weapons {

	// Use this for initialization
	void Start () {
		fireRate = 0.3f;
		lastFire = Time.time;
		reloadTime = 1.5f;
		lastReload = Time.time - reloadTime;
		magazine = 2;
		maxMagazine = 2;
		ammo = 20;
		maxAmmo = 20;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override void shoot() {
		if (magazine > 0 && Time.time > fireRate + lastFire && Time.time > reloadTime + lastReload) {
			base.shoot ();
			newBullet.GetComponent<Bullet> ().SetMovement (new LinearMovement ());
			newBullet.GetComponent<Bullet> ().SetDemage (4);
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
		if (ammo + 6 > maxAmmo) {
			ammo = maxAmmo;
		} else {
			ammo += 6;
		}
	}
}