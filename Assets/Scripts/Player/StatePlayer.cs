using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class StatePlayer : MonoBehaviour {

	public Vector2 position;
	public float moveSpeed;
	public float jumpForce;

	public bool isGrounded;
	public bool isShooting;

	public IPlayerState currentState;
	public IdleState idleState;
	public RunningState runningState;
	public JumpState jumpState;

	// Use this for initialization
	void Start () {
		string jsonString = File.ReadAllText (Application.dataPath + "/Json/PlayerInfo.json");
		JsonData playerInfo = JsonMapper.ToObject(jsonString);

		moveSpeed = float.Parse (playerInfo ["moveSpeed"].ToString());
		jumpForce = float.Parse (playerInfo ["jumpForce"].ToString());

		idleState = new IdleState (this);
		runningState = new RunningState (this);
		jumpState = new JumpState (this);
		currentState = idleState;

		Debug.Log (moveSpeed);
		Debug.Log (jumpForce);
	}
	
	// Update is called once per frame
	void Update () {
//		currentState.updateState ();
	}
}
