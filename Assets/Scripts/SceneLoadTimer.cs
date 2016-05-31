using UnityEngine;
using System.Collections;

public class SceneLoadTimer : MonoBehaviour {

	void Start () {
        StartCoroutine(LoadSceneWithDelay(3, 1));
	}

    IEnumerator LoadSceneWithDelay(float seconds, int scene) {
        SceneLoader sceneLoader = GetComponent<SceneLoader>().GetComponent<SceneLoader>();
        yield return new WaitForSeconds(seconds);
        sceneLoader.BeginLoading(scene);
    }
}
