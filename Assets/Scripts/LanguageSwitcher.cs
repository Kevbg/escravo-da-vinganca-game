using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour {
    private static string currentLanguage;
    private string currentScene;

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

    // Define o idioma que será utilizado; Não funciona no inspector
    public void SetLanguage(Languages language) {
        currentLanguage = language.ToString();
        GameControl.language = language.ToString();
    }

    public void SetLanguage(string language) {
        // É preciso verificar se a string é válida (corresponde a um elemento no enum)
        if (System.Enum.IsDefined(typeof(Languages), language)) {
            currentLanguage = language;
            GameControl.language = language;
        } else {
            throw new System.ArgumentException("Not a (supported) language", language);
        }
    }

    // Importante: A key(item) usada para pegar o texto de cada elemento é igual ao nome do Game Object
    // Portanto, deve estar no arquivo "Strings.json" com o mesmo nome que está no editor
    public void SetMenuText() {
        currentScene = GameControl.current.scene.name;
        Component[] texts = GetComponentsInChildren<Text>();

        foreach(Text txt in texts) {
            txt.text = FetchString(txt.name);
        }
    }

    public void SetDialogueText() {
        currentScene = GameControl.current.scene.name;
        Component[] texts = GetComponentsInChildren<Text>();
        foreach(Text txt in texts) {
            txt.text = FetchString(txt.GetComponentInParent<CharacterDialogue>().currentDialogueKey);
        }
    }

    public string FetchString(string item) {
        JsonParser parser = GameObject.FindGameObjectWithTag("GameController")
                            .GetComponent<JsonParser>();
        return parser.GetData(currentScene, item, currentLanguage).ToString();
    }
}
