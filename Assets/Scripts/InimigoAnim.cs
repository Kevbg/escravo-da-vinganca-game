using UnityEngine;
using System.Collections;

public class InimigoAnim : MonoBehaviour {
    private Animator animator;
    private Player player;
    private Enemy enemy;

	void Start () {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GetComponent<Enemy>();
	}
	
	void Update () {
        if (player.isDead || enemy.isDead) {
            animator.SetBool("idle", true);
            animator.SetBool("running", false);
        } else {

            if (enemy.currentState.ToString() == "EnemyIdleState" ||
                enemy.currentState.ToString() == "EnemyAlertState") {
                animator.SetBool("idle", true);
            } else {
                animator.SetBool("idle", false);
            }

            if (enemy.currentState.ToString() == "EnemyRunningState" ||
                enemy.currentState.ToString() == "EnemyPatrolState") {
                animator.SetBool("running", true);
            } else {
                animator.SetBool("running", false);
            }
        }
    }
}
