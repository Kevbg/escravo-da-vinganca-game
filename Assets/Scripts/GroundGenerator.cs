using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroundGenerator : MonoBehaviour {
    public float maxDistance;
    public float groundWidth { get; private set; }
    private List<GameObject> grounds;
    private GameObject player;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        grounds = GameObject.FindGameObjectsWithTag("Ground").ToList();
        groundWidth = currentGround().GetComponent<BoxCollider2D>().size.x;
    }
    
	void Update () {
        if (!HasBeenGenerated(groundWidth) && Input.GetAxisRaw("Horizontal") > 0) {
            GenerateGround(3);
        }
    }

    GameObject currentGround() {
        if (grounds.Count > 0) {
            return grounds[grounds.Count - 1];
        } else {
            return grounds[0];
        }
    }

    void GenerateGround(int amount) {
        float offset = 0.01f;
        float posX;

        if (player.transform.position.x < maxDistance) {
            for (int i = 0; i < amount; i++) {
                posX = currentGround().transform.position.x + groundWidth;
                print("Generating ground...");

                GameObject newGround = (GameObject)Instantiate(currentGround(), 
                    new Vector3(posX - offset, currentGround().transform.position.y), 
                    Quaternion.identity);

                newGround.name = ("Ground");
                grounds.Add(newGround);
            }
        } else if (player.transform.position.x >= maxDistance) {
            print("maxDistance reached.");
        }
    }

    public void RemoveFromList(GameObject ground) {
        grounds.Remove(ground);
    }

    public bool HasBeenGenerated(float posX) {
        float yOffset = -20f;

        if (Physics2D.OverlapPoint(new Vector2(player.transform.position.x + posX, yOffset)) != null) {
            return true;
        } else {
            return false;
        }
    }
}
