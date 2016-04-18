using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapons : MonoBehaviour {
	public GameObject bullet;

	protected Transform spawWeapon;

	protected float fireRate;
	protected float lastFire;
	protected float reloadTime;
	protected float lastReload;
	protected int magazine;
	protected int maxMagazine;
	protected int ammo;
	protected int maxAmmo;

	protected float direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void shoot() {
		
	}

	public virtual void reload() {
		if (ammo > 0) {
			Debug.Log ("reloading...");
			lastReload = Time.time;
			if (ammo > maxMagazine) {
				ammo -= (maxMagazine - magazine);
				magazine = maxMagazine;
			} else {
				magazine = ammo;
				ammo = 0;
			}
		} else {
			Debug.Log ("Without ammo...");
		}
	}

	public virtual void selectWeapon() {

	}

	public void SetDirection(float direction) {
		this.direction = direction;
	}
}
