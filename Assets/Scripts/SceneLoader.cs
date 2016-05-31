using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    public static bool isLoading { get; private set; }
    public float fadeDuration = 1.5f;

    // Não carrega a cena, só inicia a corotina que irá carregá-la (OnClick wrapper)
    public void BeginLoading(int level) {
        // O jogo não pode estar em pausa pois a cena não será carregada (timescale = 0)
        if (MenuController.gamePaused) {
            MenuController mc = GameObject.FindGameObjectWithTag("MenuPanel").GetComponent<MenuController>();
            mc.Resume();
        }

        isLoading = true;
        StartCoroutine(LoadScene(level));
    }

    // Corotina para sincronizar o fade in/out com o carregamento da cena
    public IEnumerator LoadScene(int level) {
        if (GameObject.FindGameObjectWithTag("BGM") != null) {
            GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMControl>().FadeOut();
        }
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        sf.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        sf.FadeIn(fadeDuration);
        isLoading = false;
    }
}
