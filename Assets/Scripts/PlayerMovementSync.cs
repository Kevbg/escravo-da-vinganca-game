using UnityEngine;
using System.Collections;

// Script básico para sincronizar um objeto(camera, background, etc) com o movimento do player no eixo X
public class PlayerMovementSync : MonoBehaviour {
    private Transform player;
    private Vector3 newPosition;

	void Start () {
        newPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
        newPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * Player.DefaultSpeed);
	}
}
