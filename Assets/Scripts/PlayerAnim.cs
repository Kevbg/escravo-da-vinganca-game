using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {
    private Player player;
    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void Update () {
        if (player.isDead) {
            animator.SetBool("idle", true);
            animator.SetBool("running", false);
            animator.SetBool("jumping", false);
        } else {
            if (player.currentState.ToString() == "IdleState") {
                animator.SetBool("idle", true);
            } else {
                animator.SetBool("idle", false);
            }

            if (player.currentState.ToString() == "RunningState") {
                animator.SetBool("running", true);
            } else {
                animator.SetBool("running", false);
            }

            if (player.currentState.ToString() == "JumpingState") {
                animator.SetBool("jumping", true);
            } else {
                animator.SetBool("jumping", false);
            }
        }
    }
}
