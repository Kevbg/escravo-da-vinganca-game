using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour {
    private static string currentLanguage;
    private string currentScene;
    private Component[] texts;

    public enum Languages {
        english,
        portuguese
    }

    void Start() {
        // Carrega o idioma salvo no arquivo de preferências
        try {
            SetLanguage(GameControl.language);
        } catch (System.ArgumentException) {
            SetLanguage(Languages.english);
            throw;
        }

        currentScene = GameControl.current.scene.name;
    }

    public static string GetLanguage() {
        return currentLanguage;
    }

    // Define o idioma que será utilizado e atualiza o texto; Não funciona no inspector
    public void SetLanguage(Languages language) {
        currentLanguage = language.ToString();
        GameControl.language = language.ToString();
        SetText();
    }

    public void SetLanguage(string language) {
        // É preciso verificar se a string é válida (corresponde a um elemento no enum)
        if (System.Enum.IsDefined(typeof(Languages), language)) {
            currentLanguage = language;
            GameControl.language = language;
            SetText();
        } else {
            throw new System.ArgumentException("Not a (supported) language", language);
        }
    }

    // Percorre os filhos deste gameObject e atribui o texto apropriado à eles
    // Importante: Todos os elementos "Text" que são filhos do obj com este script
    // Devem estar presentes no arquivo "Strings.json" com o mesmo nome que está no editor
    public void SetText() {
//<<<<<<< Updated upstream
//        currentScene = GameControl.current.scene.name;
//        texts = gameObject.GetComponentsInChildren<Text>();
//=======
//        buttons = gameObject.GetComponentsInChildren<Button>();
//        sliders = gameObject.GetComponentsInChildren<Slider>();
//
//        foreach(Button btn in buttons) {
//			btn.GetComponentInChildren<Text>().text = FetchString(Scenes.menu, btn.name);
//        }
//>>>>>>> Stashed changes

        foreach(Text txt in texts) {
            txt.text = FetchString(currentScene, txt.name);
        }
    }

    string FetchString(string scene, string item) {
        return GetComponent<JsonParser>().GetData(scene, item, currentLanguage).ToString();
    }
}
