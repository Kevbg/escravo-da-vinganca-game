using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private string notTarget;

	private IMovement movementType;
	private float maxLifeTime;
	private float lifeTime;

	private float direction;

	// Use this for initialization
	public void Start () {
		maxLifeTime = 3;
		lifeTime = 0;
	}
	
	// Update is called once per frame
	public void Update () {
		if (movementType != null) {
			movementType.move (gameObject, direction);
			lifeTime += Time.deltaTime;

			if (lifeTime > maxLifeTime) {
				Destroy (gameObject, 2f);
			}
		}
	}

	public void SetNotTarget(string notTarget) {
		this.notTarget = notTarget;
	}

	public void SetMovement(IMovement movementType) {
		this.movementType = movementType;
	}

	public void SetDirection (float direction) {
		this.direction = direction;
	}

	public void IgnoreCollision (Collider2D ownerCollider) {
		Physics2D.IgnoreCollision (GetComponent<Collider2D>(), ownerCollider);
	}
		
	public void OnCollisionEnter2D(Collision2D collision) {
		if (!(collision.gameObject.name == notTarget)) {
			Destroy (gameObject);
		}
	}
}
