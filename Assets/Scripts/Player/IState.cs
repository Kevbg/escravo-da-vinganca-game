using UnityEngine;
using System.Collections;

public interface IState {

	// Use this for initialization
	void Start (Entities owner);
	
	// Update is called once per frame
	void Update ();
}
