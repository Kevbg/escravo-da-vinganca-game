using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {
	[SerializeField]public List<GameObject> weapons;

	protected int activeWeapon;

	// Use this for initialization
	void Start () {
		this.activeWeapon = -1;

		this.addWeapon (0);
		this.addWeapon (1);
		this.changeWeapon (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeWeapon (int change) {
		if (activeWeapon != -1) {
			weapons [activeWeapon].GetComponent<SpriteRenderer> ().enabled = false;
		}

		if (activeWeapon == -1) {
			activeWeapon = 0;
		} else if (activeWeapon + change > weapons.Count - 1) {
			activeWeapon = 0;
		} else if (activeWeapon + change < 0) {
			activeWeapon = weapons.Count - 1;
		} else {
			activeWeapon += change;
		}
			
		weapons [activeWeapon].GetComponent<Weapons>().selectWeapon ();
	}

	public void shoot () {
		if (activeWeapon == -1) {
			Debug.Log ("No weapons selected");
		} else {
			weapons [activeWeapon].GetComponent<Weapons> ().shoot ();
		}
	}

	public void reload () {
		weapons [activeWeapon].GetComponent<Weapons>().reload ();
	}

	public void addWeapon (int weaponType) {
		weapons.Add(WeaponsController.weaponsController.CreateWeapon (weaponType, this.GetComponent<Entities> ().spawWeapon.transform));
	}
}
