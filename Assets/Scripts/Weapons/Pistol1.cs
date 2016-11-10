using UnityEngine;
using System.Collections;

public class Pistol1 : Weapons {
    public AudioClip fire;
    public AudioClip reloadSfx;
    private AudioSource sfx;

    void Start () {
		fireRate = 0.3f;
		lastFire = Time.time;
		reloadTime = 1.5f;
		lastReload = Time.time - reloadTime;
		magazine = 2;
		maxMagazine = 2;
		ammo = 60;
		maxAmmo = 60;

        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

	void Update () {
		
	}

	public override void shoot() {
		if (magazine > 0 && Time.time > fireRate + lastFire && Time.time > reloadTime + lastReload) {
			base.shoot ();
			newBullet.GetComponent<Bullet> ().SetMovement (new LinearMovement ());
			newBullet.GetComponent<Bullet> ().SetDemage (4);

            sfx.PlayOneShot(fire);
        } else if (magazine == 0) {
			this.reload ();
		} else {

		}
	}

	public override void reload() {
        if (magazine < maxMagazine) {
            base.reload();
            StartCoroutine(DelayedReloadSound());
        }
    }

    public IEnumerator DelayedReloadSound() {
        yield return new WaitForSeconds(0.5f);
        sfx.PlayOneShot(reloadSfx);
    }

	public override void selectWeapon() {
		base.selectWeapon ();
	}

	public override void addAmmo() {
		base.addAmmo ();
		if (ammo + maxMagazine > maxAmmo) {
			ammo = maxAmmo;
		} else {
			ammo += maxMagazine;
		}
	}
}