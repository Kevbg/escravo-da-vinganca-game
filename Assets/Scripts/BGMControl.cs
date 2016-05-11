using UnityEngine;
using System.Collections;

public class BGMControl : MonoBehaviour {
    private AudioSource source;
    private bool fading;

	void Start () {
        source = GetComponent<AudioSource>();
        source.volume = GameControl.musicVolume;
        FadeIn();
	}
	
	void Update () {
        if (source.volume != GameControl.musicVolume && !fading) {
            source.volume = GameControl.musicVolume;
        }
	}

    public void FadeOut() {
        StartCoroutine(VolumeFadeOut());
    }

    public void FadeIn() {
        StartCoroutine(VolumeFadeIn());
    }

    IEnumerator VolumeFadeOut(float speed = 1.5f) {
        float newVolume = source.volume;

        while (newVolume > 0) {
            newVolume = Mathf.Lerp(newVolume, 0, Time.deltaTime * speed);
            source.volume = newVolume;
            fading = true;
            yield return null;
        }
        fading = false;
    }

    IEnumerator VolumeFadeIn(float speed = 1.5f) {
        float threshold = 0.05f;
        float newVolume = 0f;
        float volume = source.volume;
        source.volume = newVolume;

        while (newVolume < volume - threshold) {
            newVolume = Mathf.Lerp(newVolume, volume, Time.deltaTime * speed);
            source.volume = newVolume;
            fading = true;
            yield return null;
        }
        fading = false;
    }
}
