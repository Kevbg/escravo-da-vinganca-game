using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HighscoresDisplay : MonoBehaviour {
    public const int MaxHighscores = 10;
    public Types fieldType;
    public enum Types {
        Names,
        Scores
    }
    private List<KeyValuePair<string, int>> highscores;

    void Start () {
        highscores = new List<KeyValuePair<string, int>>();
        UpdateHighscores();
    }

    int Compare(KeyValuePair<string, int> a, KeyValuePair<string, int> b) {
        return b.Value.CompareTo(a.Value);
    }

    string GetNames() {
        List<string> names = new List<string>();

        foreach (var element in GameControl.scores) {
            highscores.Add(new KeyValuePair<string, int>(element.Key, element.Value));
        }

        highscores.Sort(Compare);
        foreach (var element in highscores) {
            names.Add(element.Key);
        }
        if (names.Count > MaxHighscores) {
            names.RemoveRange(MaxHighscores, names.Count - MaxHighscores);
        }

        return string.Join("\n", names.ToArray());
    }

    string GetScores() {
        List<int> scores = new List<int>();

        foreach (var element in GameControl.scores) {
            highscores.Add(new KeyValuePair<string, int>(element.Key, element.Value));
        }

        highscores.Sort(Compare);
        foreach (var element in highscores) {
            scores.Add(element.Value);
        }
        if (scores.Count > MaxHighscores) {
            scores.RemoveRange(MaxHighscores, scores.Count - MaxHighscores);
        }

        return string.Join("\n", scores.Select(s => s.ToString()).ToArray());
    }

    public void UpdateHighscores() {
        Text txt = GetComponent<Text>();

        switch (fieldType) {
            case Types.Names:
                txt.text = GetNames();
                break;
            case Types.Scores:
                txt.text = GetScores();
                break;
        }
    }

    public void DeleteHighscores() {
        print("Deleting score records");
        highscores.Clear();
        GameControl.scores.Clear();
        UpdateHighscores();
    }
}
