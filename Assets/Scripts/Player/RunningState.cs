using UnityEngine;
using System.Collections;

public class RunningState : IPlayerState {

	private readonly StatePlayer player;

	public RunningState (StatePlayer player) {
		this.player = player;
	}

	#region IPlayerState implementation

	public void updateState ()
	{
		throw new System.NotImplementedException ();
	}

	public void onTriggerEnter (Collider other)
	{
		throw new System.NotImplementedException ();
	}

	public void toIdleState ()
	{
		player.currentState = player.idleState;
	}

	public void toRunnigState ()
	{
		Debug.Log ("Can't change to same state");
	}

	public void toJumpState ()
	{
		player.currentState = player.jumpState;
	}

	#endregion
}
