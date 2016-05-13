using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroundGenerator : MonoBehaviour {
    public float maxDistance;
    private float edgeDistanceThreshold;
    private GameObject player;
    private List<GameObject> grounds;
    private float groundWidth;
    private float leftEdgeX;
    private float rightEdgeX;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        grounds = GameObject.FindGameObjectsWithTag("Ground").ToList();
        groundWidth = currentGround().GetComponent<BoxCollider2D>().size.x;
        edgeDistanceThreshold = groundWidth / 2.5f;

        if (maxDistance == 0) {
            maxDistance = 500f;
        }
    }
    
	void Update () {
        if (!HasBeenGenerated() && Input.GetAxisRaw("Horizontal") > 0) {
            GenerateGround("right", 2);
        } else if (!HasBeenGenerated() && Input.GetAxisRaw("Horizontal") < 0) {
            GenerateGround("left", 2);
        }
    }

    GameObject currentGround() {
        if (grounds.Count > 0) {
            return grounds[grounds.Count - 1];
        } else {
            return grounds[0];
        }
    }

    void GenerateGround(string direction, int ammount) {
        float posX;

        if (Mathf.Abs(player.transform.position.x) < Mathf.Abs(maxDistance)) {
            for (int i = 0; i < ammount; i++) {
                if (direction == "left") {
                    posX = currentGround().transform.position.x - groundWidth;
                } else if (direction == "right") {
                    posX = currentGround().transform.position.x + groundWidth;
                } else {
                    Debug.Log("Invalid string: direction");
                    posX = 0;
                }

                print("Generating ground...");
                GameObject newGround = (GameObject)Instantiate(currentGround(), new Vector3(posX,
                                                               currentGround().transform.position.y),
                                                               Quaternion.identity);
                newGround.name = ("Ground " + i);
                grounds.Add(newGround);
            }
        } else if (Mathf.Abs(player.transform.position.x) >= Mathf.Abs(maxDistance)) {
            print("maxDistance reached.");
        }
    }

    bool HasBeenGenerated() {
        float[] posX = new float[grounds.Count];
        for (int i = 0; i < grounds.Count; i++) {
            posX[i] = grounds[i].transform.position.x;
        }

        leftEdgeX = posX.Min() - groundWidth / 2;
        rightEdgeX = posX.Max() + groundWidth / 2;

        if (player.transform.position.x + edgeDistanceThreshold > rightEdgeX ||
            player.transform.position.x - edgeDistanceThreshold < leftEdgeX) {
            return false;
        } else {
            return true;
        }
    }
}
