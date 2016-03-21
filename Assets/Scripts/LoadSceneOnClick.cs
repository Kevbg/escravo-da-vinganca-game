using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneOnClick : MonoBehaviour {
    public float fadeDuration = 1.5f;

    // Não carrega a cena, só inicia a corotina que irá carregá-la (OnClick wrapper)
    public void BeginLoading(int level) {
        StartCoroutine(LoadScene(level));
    }

    // Corotina para sincronizar o fade in/out com o carregamento da cena
    public IEnumerator LoadScene(int level) {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        sf.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        sf.FadeIn(fadeDuration);
    }
}
