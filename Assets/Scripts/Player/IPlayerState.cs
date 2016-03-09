using UnityEngine;
using System.Collections;

public interface IPlayerState {

	void updateState ();
	void onTriggerEnter (Collider other);
	void toIdleState ();
	void toRunnigState ();
	void toJumpState ();

}
