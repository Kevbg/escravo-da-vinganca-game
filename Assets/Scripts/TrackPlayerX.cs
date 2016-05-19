using UnityEngine;
using System.Collections;

// Script básico para sincronizar um objeto(camera, background, etc) com o movimento do player no eixo X
public class TrackPlayerX : MonoBehaviour {
    private GameObject player;
    private Vector3 newPosition;

	void Start () {
        newPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
        if (player.transform.position.x > 0) {
            Track();
        }
	}

    void Track() {
        newPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
