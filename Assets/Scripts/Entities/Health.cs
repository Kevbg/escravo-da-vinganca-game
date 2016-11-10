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
		Entities entity = gameObject.GetComponent<Entities> ();
		if (!entity.invulnerable) {
			entity.health -= demage;
            entity.invulnerable = true;
            StartCoroutine(entity.blink(12));
			//entity.invulnerable = true;
		}

		if (entity.health <= 0) {
			entity.health = 0;
			entity.noHealth ();
		}
	}
}
