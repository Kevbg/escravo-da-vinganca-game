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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CreateWeapon (int weaponType, Transform parent) {
		GameObject newWeapon = Instantiate (Weapons [weaponType], parent.position, Quaternion.identity) as GameObject;
		newWeapon.transform.parent = parent;
		return newWeapon;
	}

	public void CreatePickupAmmo (int ammoType, Transform parent) {
		Debug.LogWarning ("Ammo: " + ammoType);
		GameObject newPickup = Instantiate (PickupAmmoType [ammoType], parent.position, Quaternion.identity) as GameObject;
		pickupAmmo.Add (newPickup);
	}
}
