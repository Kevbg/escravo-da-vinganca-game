using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    // Aplica o fade out, com a duração especificada e parametros opcionais
    public void FadeOut(float duration, bool disableMouseClicks = true, float maxAlpha = 255) {
        GetComponent<Image>().CrossFadeAlpha(maxAlpha, duration, true);

        if (disableMouseClicks) {
            StartCoroutine(DisableMouseClicks(duration));
        }
    }

    // Aplica o fade in, com a duração especificada e parametros opcionais
    public void FadeIn(float duration, bool disableMouseClicks = true, float minAlpha = 0) {
        GetComponent<Image>().CrossFadeAlpha(minAlpha, duration, true);

        if (disableMouseClicks) {
            StartCoroutine(DisableMouseClicks(duration));
        }
    }

    // Impede que outros botões sejam pressionados enquanto o efeito não terminar
    public IEnumerator DisableMouseClicks(float duration) {
        GetComponent<Image>().raycastTarget = true;
        yield return new WaitForSeconds(duration);
        GetComponent<Image>().raycastTarget = false;
    }
}
