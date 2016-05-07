using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	private IMovement movementType;
	private float maxLifeTime;
	private float lifeTime;
	private string ownertag;
	private int demage = 1;

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

	public void SetMovement(IMovement movementType) {
		this.movementType = movementType;
	}

	public void SetDirection (float direction) {
		this.direction = direction;
	}

	public void SetOwnertag (string tag) {
		this.ownertag = tag;
	}

	public void SetDemage (int demage) {
		this.demage = demage;
	}
		
	public void OnTriggerEnter2D (Collider2D collider) {
		if (!collider.isTrigger && !collider.gameObject.CompareTag(ownertag)) {
			Destroy (gameObject);
			collider.GetComponentInParent<Health> ().takeDemage (this.demage);
		}
	}
}
