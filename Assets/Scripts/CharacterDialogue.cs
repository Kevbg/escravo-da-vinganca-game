using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterDialogue : MonoBehaviour {
    public characters character;
    public int currentDialogueIndex { get; private set; }
    public string currentDialogueKey { get; private set; }
    private LanguageSwitcher ls;

    public enum characters {
        Joaqs,
        Capataz
    }

	void Start () {
        ls = GetComponentInParent<LanguageSwitcher>();
        SetDialogue(0);
	}

    public string GetDialogue(int index) {
        currentDialogueIndex = index;
        currentDialogueKey = character.ToString() + index.ToString();
        return ls.FetchString(currentDialogueKey);
    }

    public void SetDialogue(int index) {
        GetComponentInChildren<Text>().text = GetDialogue(index);
    }
}

// Botões para debug (inspector)
[CustomEditor(typeof(CharacterDialogue))]
public class CharacterDialogEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        CharacterDialogue cd = (CharacterDialogue)target;
        if (GUILayout.Button("Next Dialogue Line")) {
            cd.SetDialogue(cd.currentDialogueIndex + 1);
        }

        if (GUILayout.Button("Reset Dialogue")) {
            cd.SetDialogue(0);
        }
    }
}