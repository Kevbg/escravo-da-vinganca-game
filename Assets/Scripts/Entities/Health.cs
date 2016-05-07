using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeDemage(int demage) {
		Entities entity = this.gameObject.GetComponent<Entities> ();
		if (entity.health - demage <= 0) {
			entity.health = 0;
			entity.noHealth ();
		} else {
			entity.health -= demage;
		}
	}
}
