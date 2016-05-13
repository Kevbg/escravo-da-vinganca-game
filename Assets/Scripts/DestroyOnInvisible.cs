using UnityEngine;
using System.Collections;

public class DestroyOnInvisible : MonoBehaviour {

	void Start () {
	    
	}
	
	void Update () {
    	
	}

    void OnBecameInvisible() {
        print("Destroying " + transform.parent.name);
        Destroy(transform.parent.gameObject);
    }
}
