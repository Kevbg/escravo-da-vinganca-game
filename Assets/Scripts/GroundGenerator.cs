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
        groundWidth = currentGround().transform.localScale.x;
        edgeDistanceThreshold = groundWidth / 3;

        if (maxDistance == 0) {
            maxDistance = 500f;
        }
    }
    
	void Update () {
        if (!HasBeenGenerated() && Player.getDirection() > 0) {
            GenerateGround(currentGround().transform.position.x + groundWidth);
        } else if (!HasBeenGenerated() && Player.getDirection() < 0) {
            GenerateGround(currentGround().transform.position.x - groundWidth);
        }
    }

    GameObject currentGround() {
        if (grounds.Count > 0) {
            return grounds[grounds.Count - 1];
        } else {
            return grounds[0];
        }
    }

    void GenerateGround(float posX) {
        if (!HasBeenGenerated() && Mathf.Abs(player.transform.position.x) < Mathf.Abs(maxDistance)) {
            print("Generating ground...");
            GameObject newGround = 
                (GameObject)Instantiate(currentGround(), new Vector3(posX, 
                                                                     currentGround().transform.position.y), 
                                                                     Quaternion.identity);
            newGround.name = ("Ground");
            grounds.Add(newGround);
        } else if (Mathf.Abs(player.transform.position.x) >= Mathf.Abs(maxDistance)) {
            print("maxDistance reached.");
        }
        DestroyOldObjects();
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

    void DestroyOldObjects() {
        if (grounds.Count % 3 == 0) {
            Destroy(grounds[grounds.Count - 3]);
            grounds.RemoveAt(grounds.Count - 3);
        }
    }
}
