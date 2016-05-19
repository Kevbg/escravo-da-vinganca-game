using UnityEngine;
using System.Collections;

public class DestroyOnInvisible : MonoBehaviour {
    private GroundGenerator groundGen;
    void Start() {
        groundGen = GameObject.FindGameObjectWithTag("GroundGenerator").GetComponent<GroundGenerator>();
    }

    void OnBecameInvisible() {
        GameObject parent = transform.parent.gameObject;
        print("Destroying " + parent.name);
        Destroy(parent);
        groundGen.RemoveFromList(parent);
    }
}
