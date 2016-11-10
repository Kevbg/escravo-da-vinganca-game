using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Entities {
	public float moveSpeed;
	public float vely;
	public const float DefaultSpeed = 30;
    public int maxHealth;
    public bool isDead { get; private set; }
	private static float xAxis;
    private SceneLoader sceneLoader;
    private EnemySpawner spawner;

	public GameObject enemyRadar;
	public bool isFacingPlayer = false;

	void Start () {
		base.onStart ();
		moveSpeed = DefaultSpeed;
        sceneLoader = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<SceneLoader>();

        if (tag == "Capataz") {
            maxHealth = 30;
        } else {
            maxHealth = Random.Range(3, 6);
            spawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        }

        health = maxHealth;
		currentState = new EnemyIdleState (this);
	}

	void Update () {
		base.onUpdate ();
		vely = GetComponent<Rigidbody2D> ().velocity.y;

		if (isFacingPlayer) {
			currentState = new EnemyAlertState (this);
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
		Vector2 newPosition = new Vector2(transform.position.x + this.getDirection(), transform.position.y);
		transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * speed);
	}

	public override void noHealth ()
	{
		base.noHealth ();
        isDead = true;

        if (tag == "Capataz") {
            print("Boss is dead");
            GetComponent<Enemy>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
            transform.position = new Vector3(transform.position.x, transform.position.y - 5f);
            transform.Rotate(0, 0, 90);
            sceneLoader.BeginLoading(GameControl.Scenes.mansaoFim);
        } else {
            float chance = Random.Range(0f, 1f);
            if (chance > 0.5f && GetComponent<Shoot>().weaponType == 1) {
                WeaponsController.weaponsController.CreatePickupAmmo(1, this.transform);
            } else if (chance > 0.33f && GetComponent<Shoot>().weaponType == 0) {
                WeaponsController.weaponsController.CreatePickupAmmo(0, this.transform);
            }

            if (GameObject.FindGameObjectWithTag("Score") != null) {
                int bonus = 0;
                if (GetComponent<Shoot>().weaponType == 1) {
                    bonus = 5;
                }
                GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>().AddScore(10 + maxHealth + bonus);
            }
            spawner.DestroyEnemy(gameObject);
        }
	}
}