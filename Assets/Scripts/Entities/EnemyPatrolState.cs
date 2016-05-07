using UnityEngine;
using System.Collections;

public class EnemyPatrolState : IState {
	Entities owner;

	protected float patrolTime;
	protected float maxPatrolTime;

	public float distance;
	public float maxDistance;

	public EnemyPatrolState(Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Enemy Patrol");
		patrolTime = 0;
		maxPatrolTime = Random.Range (1, 5.0f);
		maxDistance = Random.Range (30.0f, 50.0f);
	}
	
	// Update is called once per frame
	public void Update () {
		if (patrolTime > maxPatrolTime) {
			owner.currentState = new EnemyIdleState (this.owner);
		}

		patrolTime += Time.deltaTime;

		distance += (Time.deltaTime * 30);

		if (distance > maxDistance) {
			Vector3 scale = owner.transform.localScale;
			scale.x *= -1;
			owner.transform.localScale = scale;

			distance = 0;
		}

		owner.move ();
	}
}
