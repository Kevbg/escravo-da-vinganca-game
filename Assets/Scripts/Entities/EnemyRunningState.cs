using UnityEngine;
using System.Collections;

public class EnemyRunningState : IState {
	Entities owner;

	protected float runningTime;
	protected float maxRunningTime;

	public EnemyRunningState(Entities owner) {
		this.Start (owner);
	}

	// Use this for initialization
	public void Start (Entities owner) {
		this.owner = owner;
		Debug.Log ("Enemy Running");
		runningTime = 0;
		maxRunningTime = Random.Range (2.0f, 4.0f);

		if (owner.GetComponent<Enemy> ().isFacingPlayer) {
			owner.GetComponent<Enemy> ().isFacingPlayer = false;
			Vector3 scale = owner.transform.localScale;
			scale.x *= -1;
			owner.transform.localScale = scale;
		}
	}

	// Update is called once per frame
	public void Update () {
		owner.move ();
		runningTime += Time.deltaTime;

		if (runningTime >= maxRunningTime) {
			owner.currentState = new EnemyIdleState (owner);
		}

	}
}