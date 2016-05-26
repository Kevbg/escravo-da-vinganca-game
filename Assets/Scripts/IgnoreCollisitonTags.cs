using UnityEngine;
using System.Collections;

public class IgnoreCollisitonTags : MonoBehaviour {
    public string[] tags;
	
	void Update () {
        foreach (string tag in tags) {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag)) {
                if (!Physics2D.GetIgnoreCollision(go.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>())
                    && go != gameObject) {
                    Physics2D.IgnoreCollision(go.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
                }
            }
        }
    }
}
