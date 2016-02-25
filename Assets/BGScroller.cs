using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {
    public static BGScroller current;
    public float scrollSpeed;

    private float BGpos;

    public void Scroll () {
        // Verifica se o player está indo para a direta
        if (Player.getDirection() > 0) {
            BGpos += scrollSpeed;

            if (BGpos > 1.0f) {
                BGpos -= 1.0f;
            }

            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(BGpos, 0);

        }
        // Verifica se o player está indo para a esquerda
        else if (Player.getDirection() < 0) { 
            BGpos -= scrollSpeed;

            if (BGpos < 1.0f) {
                BGpos += 1.0f;
            }

            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(BGpos, 0);
        }
    }

	void Start () {
	    
	}

	void Update () {
        Scroll();
	}
}
