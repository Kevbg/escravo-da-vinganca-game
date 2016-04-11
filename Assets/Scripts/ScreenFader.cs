using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {
    private static ScreenFader current;

    void Awake() {
        if (current == null) {
            DontDestroyOnLoad(gameObject);
            current = this;
        } else if (current != this) {
            Destroy(gameObject);
        }
    }

    void OnLevelWasLoaded() {
        // Anexa a câmera toda vez que uma nova cena é carregada, pois cada cena tem uma câmera diferente
        GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Aplica o fade out, com a duração especificada e parametros opcionais
    public void FadeOut(float duration, float maxAlpha = 255, bool disableMouseClicks = true) {
        GetComponent<Image>().CrossFadeAlpha(maxAlpha, duration, true);

        if (disableMouseClicks) {
            StartCoroutine(DisableMouseClicks(duration));
        }
    }

    // Aplica o fade in, com a duração especificada e parametros opcionais
    public void FadeIn(float duration, float minAlpha = 0, bool disableMouseClicks = true) {
        GetComponent<Image>().CrossFadeAlpha(minAlpha, duration, true);

        if (disableMouseClicks) {
            StartCoroutine(DisableMouseClicks(duration));
        }
    }

    // Impede que outros botões sejam pressionados durante a transição
    public IEnumerator DisableMouseClicks(float duration) {
        GetComponent<Image>().raycastTarget = true;
        yield return new WaitForSeconds(duration);
        GetComponent<Image>().raycastTarget = false;
    }
}
