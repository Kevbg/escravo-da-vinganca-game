using UnityEngine;
using System.Collections;

public class JumpState : IPlayerState {

	private readonly StatePlayer player;

	public JumpState (StatePlayer player) {
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
		player.currentState = player.runningState;
	}

	public void toJumpState ()
	{
		Debug.Log ("Can't change to same state");
	}

	#endregion
}
