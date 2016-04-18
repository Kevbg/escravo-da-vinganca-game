using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapons: MonoBehaviour {
	public List<GameObject> weapons;
	public IWeapon activeWeapon;

	public int weaponIndex;

	public void Start() {
		Debug.Log (weapons[0]);
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

		Debug.Log (activeWeapon);
		activeWeapon = weapons [weaponIndex].GetComponent<IWeapon> ();
		Debug.Log (activeWeapon);
//		this.GetComponent<SpriteRenderer> ().color = activeWeapon.onSelectedWeapon();
	}

}
