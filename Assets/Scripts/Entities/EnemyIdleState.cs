using UnityEngine;
using System.Collections;

public class EnemyIdleState : IState {
	Entities owner;

	protected float idleTime;
	protected float maxIdleTime;

	public EnemyIdleState(Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Enemy Idle");
		idleTime = 0;
		maxIdleTime = Random.Range (1, 3.0f);
	}

	// Update is called once per frame
	public void Update () {
		idleTime += Time.deltaTime;

		if (idleTime >= maxIdleTime) {
			owner.currentState = new EnemyPatrolState (owner);
		}
		
	}
}