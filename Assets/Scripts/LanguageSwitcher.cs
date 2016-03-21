using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour {
    private static string currentLanguage;
    private Component[] buttons;
    private Component[] sliders;

    public enum Languages {
        english,
        portuguese
    }

    public enum Scenes {
        menu,
        level_1
    }

    void Start() {
        // Tenta carregar a língua salva no arquivo de preferências
        // Se não for possível, inglês será setado como default
        try {
            SetLanguage(GameControl.language);
        } catch (System.ArgumentException) {
            SetLanguage(Languages.english);
            throw;
        }
    }

    public static string GetLanguage() {
        return currentLanguage;
    }

    // Define a língua que será utilizada e atualiza o texto
    // O evento On Click() do inspector não aceita enums como parâmetro, 
    // Portanto este método não é reconhecido
    public void SetLanguage(Languages language) {
        currentLanguage = language.ToString();
        GameControl.language = language.ToString();
        SetText();
    }

    // Este método é reconhecido pelo On Click(), porém como o parâmetro é uma string
    // É preciso verificar se ela é válida (corresponde a um elemento no enum)
    public void SetLanguage(string language) {
        if (System.Enum.IsDefined(typeof(Languages), language)) {
            currentLanguage = language;
            GameControl.language = language;
            SetText();
        } else {
            throw new System.ArgumentException("Not a (supported) language", language);
        }
    }

    // Percorre os filhos do gameObject e atribui o texto apropriado à eles
    // Aqui devem estar todos os elementos da interface com uma string que será traduzida
    // Importante: O nome dos elementos no editor deve ser o mesmo que está no arquivo "strings" 
    public void SetText() {
        buttons = gameObject.GetComponentsInChildren<Button>();
        sliders = gameObject.GetComponentsInChildren<Slider>();

        foreach(Button btn in buttons) {
            btn.GetComponentInChildren<Text>().text = FetchString(Scenes.menu, btn.name);
        }

        foreach(Slider slider in sliders) {
            slider.GetComponentInChildren<Text>().text = FetchString(Scenes.menu, slider.name);
        }
    }

    // Recebe os dados do parser e transforma em string
    string FetchString(Scenes scene, string item) {
        return GetComponent<JsonParser>().GetData(scene.ToString(), item, currentLanguage).ToString();
    }
}
