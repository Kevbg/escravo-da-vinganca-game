using UnityEngine;
using System.Collections;

// Script básico para sincronizar um objeto(camera, background, etc) com o movimento do player no eixo X
public class TrackPlayerX : MonoBehaviour {
    public float offset;
    public static bool isTracking;
    private GameObject player;
    private GroundGenerator gg;
    private Vector3 newPosition;
    private float playerX;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gg = GameObject.FindGameObjectWithTag("GroundGenerator").GetComponent<GroundGenerator>();
        newPosition = transform.position;
        playerX = player.transform.position.x;
	}

	void Update () {
        if (player.GetComponent<Player>().isDead) {
            isTracking = false;
        } else if (player.transform.position.x > playerX && 
                  (player.transform.position.x < (gg.maxDistance + gg.groundWidth / 2) || gg.unlimited)) {
            Track();
            isTracking = true;
            playerX = player.transform.position.x;
        } else {
            isTracking = false;
        }
	}

    void Track() {
        newPosition = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
