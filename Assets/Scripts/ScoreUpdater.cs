using UnityEngine;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScoreUpdater : MonoBehaviour {
    public int currentScore { get; private set; }
    const int MaxScore = 999999;
    private Text score;

    public enum points {
        Enemy = 10
    }

	void Start () {
        score = GetComponent<Text>();
        currentScore = int.Parse(score.text);
	}
	
    public void AddScore (int amount) {
        int newScore = int.Parse(score.text);

        newScore += amount;
        if (newScore > MaxScore) {
            newScore = MaxScore;
        }
        currentScore = newScore;
        score.text = newScore.ToString();
    }

    public void SubtractScore (int amount) {
        int newScore = int.Parse(score.text);

        newScore -= amount;
        if (newScore < 0) {
            newScore = 0;
        }
        currentScore = newScore;
        score.text = newScore.ToString();
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof (ScoreUpdater))]
public class ScoreUpdaterEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ScoreUpdater updater = (ScoreUpdater)target;
        System.Random random = new System.Random();

        if (GUILayout.Button("Add: Enemy killed")) {
            updater.AddScore((int)ScoreUpdater.points.Enemy);
        }
    }
}

#endif
