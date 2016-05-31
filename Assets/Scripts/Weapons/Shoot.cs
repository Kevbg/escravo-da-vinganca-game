using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {
	[SerializeField]public List<GameObject> weapons;

	protected int activeWeapon;
    private Weapons currentWeapon;
    public int weaponType { get; private set; }

	void Start () {
        if (tag == "Capataz") {
            this.addWeapon(1);
            weaponType = 1;
        } else if (tag == "Player"){
            this.addWeapon(0);
            this.addWeapon(1);
        } else {
            float chance = Random.Range(0, 1f);
            if (chance > 0.8f) {
                this.addWeapon(1);
                weaponType = 1;
            } else {
                this.addWeapon(0);
                weaponType = 0;
            }
        }
        this.changeWeapon(0);
    }
	
	void Update () {
		
	}

    public Weapons CurrentWeapon() {
        return weapons[activeWeapon].GetComponent<Weapons>();
    }

    public void changeWeapon(int weapon) {
        foreach(GameObject go in weapons) {
            go.GetComponent<Weapons>().deselectWeapon();
        }
        activeWeapon = weapon;
        CurrentWeapon().selectWeapon();
    }

	public void shoot () {
		if (activeWeapon == -1) {
			Debug.Log ("No weapons selected");
		} else {
            CurrentWeapon().shoot ();
		}
	}

	public void reload () {
        CurrentWeapon().reload ();
	}

	public void addWeapon (int weaponType) {
		weapons.Add(WeaponsController.weaponsController.CreateWeapon (weaponType, this.GetComponent<Entities> ().spawWeapon.transform));
	}

	public void addAmmo (int ammoType) {
		weapons [ammoType].GetComponent<Weapons> ().addAmmo ();
	}
}
