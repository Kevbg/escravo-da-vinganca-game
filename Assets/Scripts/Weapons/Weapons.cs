using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapons : MonoBehaviour {
	[SerializeField]protected GameObject bullet;
	protected GameObject newBullet;

	[SerializeField]protected Transform spawBullet;

	protected float fireRate;
	protected float lastFire;
	protected float reloadTime;
	protected float lastReload;
	protected int magazine;
	protected int maxMagazine;
	protected int ammo;
	protected int maxAmmo;

	void Start () {
		
	}
	
	void Update () {
	}

	public virtual void shoot() {
        if (!MenuController.gamePaused) {
            lastFire = Time.time;
            magazine -= 1;

            newBullet = Instantiate(bullet, this.spawBullet.position, Quaternion.identity) as GameObject;
            newBullet.GetComponent<Bullet>().SetOwnertag(this.GetComponentInParent<Entities>().tag);
            newBullet.GetComponent<Bullet>().SetDirection((float)this.GetComponentInParent<Entities>().getDirection());
        }
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
		this.GetComponent<SpriteRenderer> ().enabled = true;
	}

	public virtual void deselectWeapon() {
		this.GetComponent<SpriteRenderer> ().enabled = false;
	}

	public virtual void addAmmo() {
		
	}

    public int GetAmmo() {
        return ammo;
    }

    public int GetMagazine() {
        return magazine;
    }
}
