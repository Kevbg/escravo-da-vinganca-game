using UnityEngine;
using System.Collections;

public class SceneLoadTimer : MonoBehaviour {
    public float time = 3;

	void Start () {
        StartCoroutine(LoadSceneWithDelay(time, GameControl.Scenes.menu));
	}

    IEnumerator LoadSceneWithDelay(float seconds, GameControl.Scenes scene) {
        SceneLoader sceneLoader = GetComponent<SceneLoader>().GetComponent<SceneLoader>();
        yield return new WaitForSeconds(seconds);
        sceneLoader.BeginLoading(scene);
    }
}
