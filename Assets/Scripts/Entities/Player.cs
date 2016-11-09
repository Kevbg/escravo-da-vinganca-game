using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Entities {
	public float moveSpeed;
	public float vely;
	public const float DefaultSpeed = 30;
    public bool isDead { get; private set; }
    public const int MaxHealth = 12;
	private static float xAxis;
    private Shoot shootComponent;
    private MenuScript menu;
    private HotbarController hotbar;
    private bool inputDisabled;

	void Start () {
		base.onStart ();
		moveSpeed = DefaultSpeed;
        health = MaxHealth;

		currentState = new IdleState (this);
        menu = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<MenuScript>();
        hotbar = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<HotbarController>();
        shootComponent = GetComponent<Shoot>();
	}

	void Update () {
        if (!inputDisabled && Time.timeScale > 0) {
            if (Input.GetAxisRaw("Fire1") > 0) {
                shootComponent.shoot();
            }
            if (Input.GetAxisRaw("Reload1") > 0) {
                shootComponent.reload();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                shootComponent.changeWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                shootComponent.changeWeapon(1);
            }

            xAxis = Input.GetAxisRaw("Horizontal");

            hotbar.UpdateAmmoCount(shootComponent.CurrentWeapon().GetMagazine(),
                                       shootComponent.CurrentWeapon().GetAmmo());
        }
	}

    void FixedUpdate() {
        base.onUpdate();
        vely = GetComponent<Rigidbody2D>().velocity.y;

        if (xAxis > 0 && this.transform.localScale.x < 0) {
            Vector3 scale = this.transform.localScale;
            scale.x *= -1;
            this.transform.localScale = scale;
        } else if (xAxis < 0 && this.transform.localScale.x > 0) {
            Vector3 scale = this.transform.localScale;
            scale.x *= -1;
            this.transform.localScale = scale;
        }
    }

	void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

	public override void move (float speed = 30) {
		Vector2 newPosition = new Vector2(transform.position.x + xAxis, transform.position.y);
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * speed);
	}

    public override void noHealth() {
        print("Player is dead!");
        DisableEnemyAI();
        isDead = true;
        inputDisabled = true;
        invulnerableTime = 0;
        invulnerable = false;
        transform.position = new Vector3(transform.position.x, transform.position.y -5f);
        transform.Rotate(0, 0, 90);

        foreach(GameObject bullet in GameObject.FindGameObjectsWithTag("Fire")) {
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

        menu.GameOverPause();
    }

    void DisableEnemyAI() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) {
            enemy.GetComponent<Enemy>().enabled = false;
        }

        if (GameObject.FindGameObjectWithTag("Capataz") != null) {
            GameObject.FindGameObjectWithTag("Capataz").GetComponent<Enemy>().enabled = false;
        }
    }
}
