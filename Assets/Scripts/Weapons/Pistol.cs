using UnityEngine;
using System.Collections;

public class Pistol : Weapons {
    public AudioClip fire;
    public AudioClip reloadSfx;
    private AudioSource sfx;

	void Start () {
		fireRate = 0.2f;
		lastFire = Time.time;
		reloadTime = 1.2f;
		lastReload = Time.time - reloadTime;
		magazine = 6;
		maxMagazine = 6;
		ammo = 120;
		maxAmmo = 120;

        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }
	
	void Update () {
		
	}

	public override void shoot() {
		if (magazine > 0 && Time.time > fireRate + lastFire && Time.time > reloadTime + lastReload) {
			base.shoot ();
			newBullet.GetComponent<Bullet> ().SetMovement (new LinearMovement ());
			newBullet.GetComponent<Bullet> ().SetDemage (2);

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
        yield return new WaitForSeconds(0.1f);
        sfx.PlayOneShot(reloadSfx);
    }

    public override void selectWeapon() {
		base.selectWeapon ();
	}

	public override void addAmmo() {
		base.addAmmo ();
		if (ammo + 12 > maxAmmo) {
			ammo = maxAmmo;
		} else {
			ammo += 12;
		}
	}
}