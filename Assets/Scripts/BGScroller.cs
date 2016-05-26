using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {
    public static BGScroller current;
    public int order;
    public float scrollSpeed;
    private float BGpos;
    private float playerX;
    private GameObject player;

    void Start() {
        GetComponent<Renderer>().material.renderQueue = order;
        player = GameObject.FindGameObjectWithTag("Player");
        playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
    }

    void Update() {
        if (player.transform.position.x > playerX) {
            Scroll();
            playerX = player.transform.position.x;
        }
    }

    public void Scroll () {
        // Verifica se o player está indo para a direta
		if (!MenuController.gamePaused && Input.GetAxisRaw("Horizontal") > 0) {
            BGpos += scrollSpeed;

            if (BGpos > 1.0f) {
                BGpos -= 1.0f;
            }
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(BGpos, 0);
        }
        // Verifica se o player está indo para a esquerda
		else if (!MenuController.gamePaused && Input.GetAxisRaw("Horizontal") < 0) { 
            BGpos -= scrollSpeed;

            if (BGpos < 1.0f) {
                BGpos += 1.0f;
            }
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(BGpos, 0);
        }
    }
}
