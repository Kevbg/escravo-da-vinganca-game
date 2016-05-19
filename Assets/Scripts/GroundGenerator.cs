using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroundGenerator : MonoBehaviour {
    public float maxDistance;
    private List<GameObject> grounds;
    private GameObject player;
    private float edgeDistanceThreshold;
    private float groundWidth;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        grounds = GameObject.FindGameObjectsWithTag("Ground").ToList();
        groundWidth = currentGround().GetComponent<BoxCollider2D>().size.x;
        edgeDistanceThreshold = groundWidth / 2.5f;
    }
    
	void Update () {
        if (!HasBeenGenerated() && Input.GetAxisRaw("Horizontal") > 0) {
            GenerateGround("right", 3);
        } else if (!HasBeenGenerated() && Input.GetAxisRaw("Horizontal") < 0) {
            GenerateGround("left", 3);
        }
    }

    GameObject currentGround() {
        if (grounds.Count > 0) {
            return grounds[grounds.Count - 1];
        } else {
            return grounds[0];
        }
    }

    void GenerateGround(string direction, int amount) {
        float posX;

        if (player.transform.position.x < maxDistance) {
            for (int i = 0; i < amount; i++) {
                if (direction == "left") {
                    posX = currentGround().transform.position.x - groundWidth;
                } else if (direction == "right") {
                    posX = currentGround().transform.position.x + groundWidth;
                } else {
                    Debug.Log("Invalid direction");
                    posX = 0;
                }

                print("Generating ground...");
                GameObject newGround = (GameObject)Instantiate(currentGround(), new Vector3(posX,
                                                               currentGround().transform.position.y),
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

    bool HasBeenGenerated() {
        Collider2D col;
        float xOffset;
        float yOffset = -20f;

        if (Input.GetAxisRaw("Horizontal") > 0) {
            xOffset = edgeDistanceThreshold;
        } else {
            xOffset = -edgeDistanceThreshold;
        }

        if (Physics2D.OverlapPoint(new Vector2(player.transform.position.x + xOffset, yOffset)) != null) {
            col = Physics2D.OverlapPoint(new Vector2(player.transform.position.x + xOffset, yOffset));
            return true;
        } else {
            return false;
        }
    }
}
