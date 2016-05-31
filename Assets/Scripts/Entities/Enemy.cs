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

	public GameObject enemyRadar;
	public bool isFacingPlayer = false;

	void Start () {
		base.onStart ();
		moveSpeed = DefaultSpeed;
        sceneLoader = GameObject.FindGameObjectWithTag("MenuPanel").GetComponent<SceneLoader>();

        if (tag == "Capataz") {
            maxHealth = 30;
        } else {
            maxHealth = Random.Range(3, 6);
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
            StartCoroutine(sceneLoader.LoadScene(6));
        } else {
            float chance = Random.Range(0f, 1f);
            if (chance > 0.8f) {
                WeaponsController.weaponsController.CreatePickupAmmo(1, this.transform);
            } else if (chance > 0.5f) {
                WeaponsController.weaponsController.CreatePickupAmmo(0, this.transform);
            }

            if (GameObject.FindGameObjectWithTag("Score") != null) {
                GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>().AddScore(10 + maxHealth);
            }
            Destroy(this.gameObject);
        }
	}
}