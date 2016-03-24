using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapons: MonoBehaviour {
	public List<IWeapon> weapons;
	public IWeapon activeWeapon;

	public int weaponIndex;

	public void Start() {
		weapons = new List<IWeapon> ();
		weapons.Add (new Pistol (gameObject, 5));
		weapons.Add (new MachineGun (gameObject, 1));
		weapons.Add (new LaserGun (gameObject, 0.5f));
		weaponIndex = 0;

		changeWeapon (0);
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.W)) {
			activeWeapon.shoot();
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			changeWeapon(-1);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			changeWeapon (1);
		}
	}

	protected void changeWeapon(int change) {
		if (weaponIndex + change > weapons.Count - 1) {
			weaponIndex = 0;
		} else if (weaponIndex + change < 0) {
			weaponIndex = weapons.Count - 1;
		} else {
			weaponIndex += change;
		}

		activeWeapon = weapons [weaponIndex];
		this.GetComponent<SpriteRenderer> ().color = activeWeapon.onSelectedWeapon();
	}

}
