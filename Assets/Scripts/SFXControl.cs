using UnityEngine;
using System.Collections;

public class SFXControl : MonoBehaviour {
    private AudioSource source;

	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	void Update () {
    	if (source.volume != GameControl.sfxVolume) {
            source.volume = GameControl.sfxVolume;
        }
	}
}
