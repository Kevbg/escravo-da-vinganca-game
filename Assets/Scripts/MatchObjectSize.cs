using UnityEngine;

public class MatchObjectSize : MonoBehaviour {
    public GameObject target;

	void Start () {
        GetComponent<RectTransform>().sizeDelta = target.GetComponent<RectTransform>().sizeDelta;
	}
}
