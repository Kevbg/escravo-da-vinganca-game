using UnityEngine;
using System.Collections;

// Script básico para sincronizar um objeto(camera, background, etc) com o movimento do player no eixo X
public class PlayerMovementSync : MonoBehaviour {
    private Transform player;

	void Start () {
        player = GameObject.Find("Player").transform;
	}

	void Update () {
        Vector3 playerPos = player.position;
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
	}
}
