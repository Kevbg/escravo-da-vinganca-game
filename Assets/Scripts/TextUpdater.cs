using System;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour {
    private JsonParser parser;

    public enum Languages {
        english,
        portuguese
    }

	void Start () {
		parser = GameObject.FindGameObjectWithTag("GameController").GetComponent<JsonParser>();
    }

    public void SetLanguage(string language) {
        if (Enum.IsDefined(typeof(Languages), language)) {
            GameControl.language = language;
        } else {
            throw new ArgumentException("Not a (supported) language", language);
        }
    }

    public void UpdateText(GameObject parent) {
        Component[] texts = parent.GetComponentsInChildren<Text>();

        foreach (Text txt in texts) {
            if (txt.tag == "GameOverScore") {
                int score = GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreUpdater>().currentScore;
                txt.text = FetchItem(txt.name) + score;
            } else if (txt.tag != "IgnoreLanguage") {
                txt.text = FetchItem(txt.name);
            }
        }
    }

    public string FetchItem(string item) {
        return parser.GetItem(item, GameControl.language).ToString();
    }

    public string FetchDialogue(int index, out string character) {
        return parser.GetDialogue(index, GameControl.language, out character).ToString();
    }
}
