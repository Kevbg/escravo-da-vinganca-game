using System.Collections;
using UnityEngine;

public class ExitOnClick : MonoBehaviour {
    public float fadeDuration = 1.5f;

    // Inicia a corotina que fecha o jogo (OnClick wrapper)
    public void Exit() {
        StartCoroutine(ExitGame());
    }

    // Corotina para aplicar fade out e fechar o jogo
    public IEnumerator ExitGame() {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        sf.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

    //Diretiva de compilação para verificar se o jogo está rodando no editor da Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
