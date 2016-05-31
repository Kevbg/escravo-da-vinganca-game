using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LanguageSwitcher : MonoBehaviour {
    const int MaxHighscores = 3;
    private static string currentLanguage;

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
        Component[] texts = GetComponentsInChildren<Text>();

        foreach(Text txt in texts) {
            if (txt.tag == "GameOverScore") {
                int score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>().currentScore;
                txt.text = FetchItem(txt.name) + score;
            } else if (txt.tag != "IgnoreLanguage"){
                txt.text = FetchItem(txt.name);
            }
        }
    }

    int Compare(KeyValuePair<string, int> a, KeyValuePair<string, int> b) {
        return b.Value.CompareTo(a.Value);
    }

    public void SetDialogueText() {
        Component[] texts = GetComponentsInChildren<Text>();
        foreach (Text txt in texts) {
            if (txt.tag == "Dialogue") {
                txt.text = FetchDialogue(txt.GetComponentInParent<CharacterDialogue>().currentDialogueIndex);
            } else if (txt.tag == "NamePlate") {
                string charName;
                FetchDialogue(txt.GetComponentInParent<CharacterDialogue>().currentDialogueIndex, out charName);
                txt.text = charName;
            } else {
                txt.text = txt.text = FetchItem(txt.name);
            }
        }
    }

    public string FetchItem(string item) {
        JsonParser parser = GameObject.FindGameObjectWithTag("GameController")
                            .GetComponent<JsonParser>();
        return parser.GetItem(item, currentLanguage).ToString();
    }

    public string FetchDialogue(int index) {
        JsonParser parser = GameObject.FindGameObjectWithTag("GameController")
                            .GetComponent<JsonParser>();
        return parser.GetDialogue(index, currentLanguage).ToString();
    }

    public string FetchDialogue(int index, out string character) {
        JsonParser parser = GameObject.FindGameObjectWithTag("GameController")
                            .GetComponent<JsonParser>();
        return parser.GetDialogue(index, currentLanguage, out character).ToString();
    }
}
