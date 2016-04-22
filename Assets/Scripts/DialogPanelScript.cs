using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogPanelScript : MonoBehaviour {
    public const float DefaultLerpSpeed = 7.5f;
    public GameObject character;
    public float lerpSpeed;
    public PivotAlignment pivotAnchor;
    private PivotAlignment lastPivotAnchor;
    private RectMask2D dialogPanel;
    private Vector3 centerPivotOffset;
    private Vector3 bottomLeftPivotOffset;
    private Vector3 bottomRightPivotOffset;

    public enum PivotAlignment {
        Center,
        BottomLeft,
        BottomRight
    }

    void Start () {
        float charWidth = character.GetComponent<Renderer>().bounds.size.x;
        float charHeight = character.GetComponent<Renderer>().bounds.size.y;
        dialogPanel = GetComponent<RectMask2D>();

        centerPivotOffset = new Vector3(0, charHeight);
        bottomLeftPivotOffset = new Vector3(-charWidth /2, charHeight / 2);
        bottomRightPivotOffset = new Vector3(charWidth / 2, charHeight / 2);

        anchorPivot(pivotAnchor);
        lastPivotAnchor = pivotAnchor;

        if (lerpSpeed <= 0) {
            lerpSpeed = DefaultLerpSpeed;
        }
    }

    void LateUpdate() {
        switch (pivotAnchor) {
            case PivotAlignment.Center:
                transform.position = character.transform.position + centerPivotOffset;
                pivotAnchor = PivotAlignment.Center;
                break;
            case PivotAlignment.BottomLeft:
                transform.position = character.transform.position + bottomLeftPivotOffset;
                pivotAnchor = PivotAlignment.BottomLeft;
                break;
            case PivotAlignment.BottomRight:
                transform.position = character.transform.position + bottomRightPivotOffset;
                pivotAnchor = PivotAlignment.BottomRight;
                break;
        }

        if (lastPivotAnchor != pivotAnchor) {
            anchorPivot(pivotAnchor);
            lastPivotAnchor = pivotAnchor;
        }
    }

    void anchorPivot(PivotAlignment alignment) {
        switch (alignment) {
            case PivotAlignment.Center:
                dialogPanel.rectTransform.pivot = new Vector2(0.5f, 0.5f);
                pivotAnchor = PivotAlignment.Center;
                break;
            case PivotAlignment.BottomLeft:
                dialogPanel.rectTransform.pivot = new Vector2(1, 0);
                pivotAnchor = PivotAlignment.BottomLeft;
                break;
            case PivotAlignment.BottomRight:
                dialogPanel.rectTransform.pivot = new Vector2(0, 0);
                pivotAnchor = PivotAlignment.BottomRight;
                break;
        }
    }

    public IEnumerator HideDialogPanel() {
        float threshold = 0.025f;

        while (Vector3.Distance(dialogPanel.transform.localScale, Vector3.zero) > threshold) {
            dialogPanel.transform.localScale = Vector3.Lerp(dialogPanel.transform.localScale, 
                                                            Vector3.zero, 
                                                            Time.deltaTime * lerpSpeed);
            yield return null;
        }
        dialogPanel.transform.localScale = Vector3.zero;
    }

    public IEnumerator ShowDialogPanel() {
        float threshold = 0.01f;

        while (Vector3.Distance(dialogPanel.transform.localScale, Vector3.one) > threshold) {
            dialogPanel.transform.localScale = Vector3.Lerp(dialogPanel.transform.localScale, 
                                                            Vector3.one, 
                                                            Time.deltaTime * lerpSpeed);
            yield return null;
        }
        dialogPanel.transform.localScale = Vector3.one;
    }

    public bool? IsHidden() {
        if (dialogPanel.transform.localScale == Vector3.zero) {
            return true;
        } else if (dialogPanel.transform.localScale == Vector3.one) {
            return false;
        } else {
            return null;
        }
    }
}

// Botões para debug (inspector)
[CustomEditor(typeof(DialogPanelScript))]
public class DialogScriptEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        DialogPanelScript ds = (DialogPanelScript)target;
        if (GUILayout.Button("Show/Hide Dialog Panel")) {
            if ((bool)ds.IsHidden()) {
                ds.StartCoroutine(ds.ShowDialogPanel());
            } else if (!(bool)ds.IsHidden()) {
                ds.StartCoroutine(ds.HideDialogPanel());
            }
        }
    }
}