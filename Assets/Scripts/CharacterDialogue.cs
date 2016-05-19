using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterDialogue : MonoBehaviour {
    public int startingLine;
    public int numOfLines;
    public int nextScene;
    public int currentDialogueIndex { get; private set; }
    private LanguageSwitcher ls;
    private SceneLoader sceneLoader;
    private Sprite joaqsHead;
    private Sprite capatazHead;

    public enum characters {
        Joaqs,
        Capataz
    }

	void Start () {
        ls = GetComponentInParent<LanguageSwitcher>();
        sceneLoader = GameObject.FindGameObjectWithTag("MenuPanel").GetComponent<SceneLoader>();
        joaqsHead = Resources.Load<Sprite>("Sprites/Cabeça Joaqs");
        capatazHead = Resources.Load<Sprite>("Sprites/Cabeça Capataz");

        SetDialogue(startingLine);
        ls.SetDialogueText();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !MenuController.gamePaused) {
            if (currentDialogueIndex + 1 < numOfLines + startingLine) {
                SetDialogue(currentDialogueIndex + 1);
            } else {
                print("Loading next scene...");
                sceneLoader.BeginLoading(nextScene);
            }
        }
    }

    public void SetPortrait(Sprite newPortrait) {
        Image portrait = GameObject.FindGameObjectWithTag("Portrait").GetComponent<Image>();
        portrait.sprite = newPortrait;
    }

    public void SetCharacterName(string name) {
        Text namePlate = GameObject.FindGameObjectWithTag("NamePlate").GetComponent<Text>();
        namePlate.text = name;
    }

    public string GetDialogue(int index, out string charName) {
        currentDialogueIndex = index;
        return ls.FetchDialogue(currentDialogueIndex, out charName);
    }

    public void SetDialogue(int index) {
        string charName;
        Text dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Text>();
        dialogue.text = GetDialogue(index, out charName);

        if (charName == characters.Joaqs.ToString()) {
            SetPortrait(joaqsHead);
            SetCharacterName(charName);
        } else if (charName == characters.Capataz.ToString()) {
            SetPortrait(capatazHead);
            SetCharacterName(charName);
        }
    }
}

#if UNITY_EDITOR
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
            cd.SetDialogue(cd.startingLine);
        }
    }
}
#endif
