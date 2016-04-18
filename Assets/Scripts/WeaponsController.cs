using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsController : MonoBehaviour {
	public List<Weapons> weapons;
	public List<GameObject> weaponsObjects;

	public int activeWeapon;

	// Use this for initialization
	void Start () {
		this.weaponsObjects.Insert(0, Instantiate (weapons [0].gameObject, this.transform.position, Quaternion.identity ) as GameObject);
		this.weaponsObjects [0].transform.parent = this.transform;
		this.activeWeapon = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.parent.transform.localRotation.y == -1) {
			weaponsObjects [activeWeapon].GetComponent<Weapons> ().SetDirection (-1);
		} else if (this.transform.parent.transform.localRotation.y == 0) {
			weaponsObjects [activeWeapon].GetComponent<Weapons> ().SetDirection (1);
		}
	}

	public void changeWeapon(int change) {
		if (activeWeapon + change > weapons.Count - 1) {
			activeWeapon = 0;
		} else if (activeWeapon + change < 0) {
			activeWeapon = weapons.Count - 1;
		} else {
			activeWeapon += change;
		}
			
		weaponsObjects [activeWeapon].GetComponent<Weapons>().selectWeapon ();
	}

	public void shoot() {
		weaponsObjects [activeWeapon].GetComponent<Weapons>().shoot ();
	}

	public void reload() {
		weaponsObjects [activeWeapon].GetComponent<Weapons>().reload ();
	}
}
