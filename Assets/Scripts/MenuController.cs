using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {
    private GameObject canvas;
    private GameObject menuPanel;
    private RectMask2D[] menus;

	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        menuPanel = GameObject.FindGameObjectWithTag("MenuPanel");
        menus = menuPanel.GetComponentsInChildren<RectMask2D>(true);
        // Foi usado GetComponentsInChildren<> em vez de GameObject.FindGameObjectsWithTag()
        // Pois este método também retorna objetos inativos com o parâmetro (true)
    }

    // Habilita o menu especificado no inspector
    public void EnableMenu(RectMask2D menu) {
        // Automaticamente desabilita todos os outros menus,
        // Permitindo que apenas um único menu esteja ativo
        foreach (RectMask2D mask in menus) {
            if (mask.gameObject.activeSelf) {
                mask.gameObject.SetActive(false);
            }
        }

        menu.gameObject.SetActive(true);
        // É preciso setar o texto dos elementos do menu que será habilitado
        canvas.GetComponent<LanguageSwitcher>().SetText();
    }
}
