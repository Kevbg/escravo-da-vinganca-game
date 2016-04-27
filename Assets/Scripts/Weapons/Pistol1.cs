using UnityEngine;
using System.Collections;

public class Pistol1 : Weapons {

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
		if (this.transform.parent.parent.localEulerAngles.y > 0) {
			direction = -1;
		} else {
			direction = 1;
		}
	}

	public override void shoot() {
		base.shoot ();
		//		Debug.LogWarning (this.transform.parent.transform.parent.name);
		if (magazine > 0 && Time.time > fireRate + lastFire && Time.time > reloadTime + lastReload) {
			lastFire = Time.time;
			magazine -= 1;

			GameObject newBullet = Instantiate (bullet, this.transform.position, Quaternion.identity) as GameObject;
			newBullet.GetComponent<Bullet> ().SetMovement (new LinearMovement ());
			newBullet.GetComponent<Bullet> ().SetDirection (direction);
			newBullet.GetComponent<Bullet> ().IgnoreCollision (this.transform.parent.transform.parent.GetComponent<Collider2D> ());

		} else if (magazine == 0) {
			this.reload ();
		} else {

		}
	}

	public override void selectWeapon() {
		base.selectWeapon ();
	}
}