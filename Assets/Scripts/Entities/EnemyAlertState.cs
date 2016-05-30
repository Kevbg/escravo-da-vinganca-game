using UnityEngine;
using System.Collections;

public class EnemyAlertState : IState {
	Entities owner;

	public EnemyAlertState(Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Enemy alert");
	}

	// Update is called once per frame
	public void Update () {
		owner.GetComponent<Shoot> ().shoot ();

		if (!owner.GetComponent<Enemy> ().isFacingPlayer) {
			owner.currentState = new EnemyPatrolState (owner);
		}
		if (owner.health <= 2) {
			owner.currentState = new EnemyRunningState (owner);
		}
	}
}