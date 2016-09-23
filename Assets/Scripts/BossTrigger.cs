using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            print("Loading boss scene");
            GetComponent<SceneLoader>().BeginLoading(GameControl.Scenes.mansaoLuta);
        }
    }
}
