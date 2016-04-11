using UnityEngine;
using System.Linq;
using System.Collections;

public class HotbarController : MonoBehaviour {
    private GameObject[] slots;
    private GameObject selector;
    private bool coroutineRunning;
    public int activeSlot { get; private set; }

	void Start () {
        // Ordena os slots pela sua pos na hierarquia 'GetSiblingIndex()' para que suas posições estejam corretas
        slots = GameObject.FindGameObjectsWithTag("Slot").OrderBy(go => go.transform.GetSiblingIndex()).ToArray();
        selector = GameObject.FindGameObjectWithTag("Selector");
	}
	
	void Update () {
        // Input para debugging
        foreach (char c in Input.inputString) {
            int key = (int)char.GetNumericValue(c);

            if (!MenuController.gamePaused && key >= 1 && key <= 4) {
                MoveSelector(key - 1);
                print("Active slot: " + activeSlot);
            }
        }
    }

    public void MoveSelector(int slot) {
        activeSlot = slot;
        float lerpSpeed = 25f;
        float slotWidth = slots[activeSlot].GetComponent<RectTransform>().rect.width;
        float slotHeight = slots[activeSlot].GetComponent<RectTransform>().rect.height;
        Vector3 slotPosCenter = new Vector3(slots[activeSlot].transform.localPosition.x + slotWidth / 2, 
                                            slots[activeSlot].transform.localPosition.y + slotHeight / 2);

        if (!coroutineRunning) {
            StartCoroutine(LerpObject(selector, slotPosCenter, lerpSpeed));
        }
    }

    // Lerp para elementos da UI, usa a pos local do objeto
    IEnumerator LerpObject(GameObject go, Vector3 target, float speed) {
        coroutineRunning = true;
        float threshold = 5f;

        // Aproximação; Lerp é desnecessário quando o objeto está próximo
        while (Vector3.Distance(go.transform.localPosition, target) > threshold) {
            go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, target, Time.deltaTime * speed);
            yield return null;
        }
        go.transform.localPosition = target;
        coroutineRunning = false;
    }

    public void UpdateAmmoCount(int ammoInClip, int totalAmmo) {
        selector.GetComponentInChildren<UnityEngine.UI.Text>().text = (ammoInClip + "/" + totalAmmo);
    }
}
