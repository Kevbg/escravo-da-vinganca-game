using UnityEngine;
using System.Collections;

// Script básico para sincronizar um objeto(camera, background, etc) com o movimento do player no eixo X
public class TrackPlayerX : MonoBehaviour {
    public float offset;
    private GameObject player;
    private Vector3 newPosition;
    private float playerX;

	void Start () {
        newPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerX = player.transform.position.x;
	}

	void Update () {
        if (player.transform.position.x > playerX) {
            Track();
            playerX = player.transform.position.x;
        }
	}

    void Track() {
        newPosition = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
