using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CharacterDialogue : MonoBehaviour {
    public int startingLine;
    public int numOfLines;
    public GameControl.Scenes nextScene;
    public int currentDialogueIndex { get; private set; }
    private TextUpdater textUpdater;
    private SceneLoader sceneLoader;
    private Sprite joaqsHead;
    private Sprite capatazHead;

    public enum characters {
        Joaqs,
        Capataz
    }

	void Start () {
        textUpdater = GetComponentInParent<TextUpdater>();
        sceneLoader = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<SceneLoader>();
        joaqsHead = Resources.Load<Sprite>("Sprites/Cabeça Joaqs");
        capatazHead = Resources.Load<Sprite>("Sprites/Cabeça Capataz");

        SetDialogue(startingLine);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale > 0) {
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
        return textUpdater.FetchDialogue(index, out charName);
    }

    public void SetDialogue(int index) {
        string charName;
        Text dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Text>();
        Text continueText = GameObject.FindGameObjectWithTag("ContinueText").GetComponent<Text>();
        dialogue.text = GetDialogue(index, out charName);
        continueText.text = textUpdater.FetchItem(continueText.name);

        if (charName == characters.Joaqs.ToString()) {
            SetPortrait(joaqsHead);
            SetCharacterName(charName);
        } else if (charName == characters.Capataz.ToString()) {
            SetPortrait(capatazHead);
            SetCharacterName(charName);
        }
    }

    public void UpdateDialogue() {
        SetDialogue(currentDialogueIndex);
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
