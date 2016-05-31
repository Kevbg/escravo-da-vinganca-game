using UnityEngine;
using System.Collections.Generic;

public class WeaponsController : MonoBehaviour {
	public static WeaponsController weaponsController;

	public List<GameObject> Weapons;
	public List<GameObject> PickupAmmoType;
	private List<GameObject> pickupAmmo;

	void Awake () {
		if (!weaponsController) {
			weaponsController = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start () {
        pickupAmmo = new List<GameObject>();
	}
	
	void Update () {
	
	}

	public GameObject CreateWeapon (int weaponType, Transform parent) {
		GameObject newWeapon = Instantiate (Weapons [weaponType], parent.position, Quaternion.Euler(0, 0, 330)) as GameObject;
		newWeapon.transform.parent = parent;
		return newWeapon;
	}

	public void CreatePickupAmmo (int ammoType, Transform parent) {
		Debug.LogWarning ("Ammo: " + ammoType);
		GameObject newPickup = Instantiate (PickupAmmoType [ammoType], new Vector3(parent.position.x, -12), Quaternion.identity) as GameObject;
		pickupAmmo.Add (newPickup);
	}
}
