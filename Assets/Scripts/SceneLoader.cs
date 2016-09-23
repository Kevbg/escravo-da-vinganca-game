using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    public static bool isLoading { get; private set; }
    public float fadeDuration = 1f;

    public void BeginLoading(GameControl.Scenes scene) {
        isLoading = true;
        StartCoroutine(LoadScene(scene.ToString()));

        // Verificar se o jogo está pausado
    }

    public void BeginLoading(string scene) {
        if (Enum.IsDefined(typeof(GameControl.Scenes), scene)) {
            isLoading = true;
            StartCoroutine(LoadScene(scene));

            // Verificar se o jogo está pausado
        } else {
            throw new ArgumentException("Scene not found", scene);
        }
    }

    // Corotina para sincronizar o fade in/out com o carregamento da cena
    public IEnumerator LoadScene(string scene) {
        if (GameObject.FindGameObjectWithTag("BGM") != null) {
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMControl>().FadeOut();
        }
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        sf.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        sf.FadeIn(fadeDuration);
        isLoading = false;
    }
}
