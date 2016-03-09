using UnityEngine;
using System.Collections;

public class IdleState : IPlayerState {

	private readonly StatePlayer player;

	public IdleState (StatePlayer player) {
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
		Debug.Log ("Can't change to same state");
	}

	public void toRunnigState ()
	{
		player.currentState = player.runningState;
	}

	public void toJumpState ()
	{
		player.currentState = player.jumpState;
	}

	#endregion
}
