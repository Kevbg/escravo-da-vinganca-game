using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {
    private GameObject canvas;
    private GameObject menuPanel;
    private ScreenFader sf;
    private RectMask2D[] menus;
    private RectMask2D pauseMenu;
    public static bool gamePaused { get; private set; }

    [Range(0, 255)]
    public int alpha;
    public float fadeDuration;

	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        menuPanel = GameObject.FindGameObjectWithTag("MenuPanel");
        sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        menus = menuPanel.GetComponentsInChildren<RectMask2D>(true);
        // GetComponentsInChildren<> retorna objetos inativos com o parâmetro (true)

        pauseMenu = menus[0];
    }

    void Update() {
        // Verificão de pausa/retomada
        if (Input.GetKeyDown(KeyCode.Escape) && IsActive(pauseMenu) && gamePaused) {
            Resume();
        } else if (Input.GetKeyDown(KeyCode.Escape) && !SceneLoader.isLoading && !gamePaused) {
            Pause();
        }
    }

    public void Pause() {
        if (GameControl.current.scene.name != GameControl.Scenes.menu.ToString()) {
            print("Game paused");
            Time.timeScale = 0;
            gamePaused = true;
            FadeOut();
            EnableMenu(pauseMenu);
        }
    }

    public void Resume() {
        print("Game resumed");
        Time.timeScale = 1;
        gamePaused = false;
        FadeIn();
        DisableMenu(pauseMenu);
    }

    public void FadeOut() {
        sf.FadeOut(fadeDuration, alpha, false);
    }

    public void FadeIn() {
        sf.FadeIn(fadeDuration, 0, false);
    }

    public void EnableMenu(RectMask2D menu) {
        // Automaticamente desabilita todos os outros menus,
        // Permitindo que apenas um único menu esteja ativo
        foreach (RectMask2D mask in menus) {
            if (IsActive(mask)) {
                DisableMenu(mask);
            }
        }

        menu.gameObject.SetActive(true);

        // É preciso setar o texto dos elementos do menu que será habilitado
        canvas.GetComponent<LanguageSwitcher>().SetText();

        // Controla as barras de volume (se existirem no menu) pela sua pos na hierarquia
        foreach (Slider slider in menu.GetComponentsInChildren<Slider>()) {

            if (slider.transform.GetSiblingIndex() == 0) {
                slider.value = GameControl.sfxVolume;
                slider.onValueChanged.AddListener(SetSFXVolume);
                SetSFXVolume(slider.value);
            } else if (slider.transform.GetSiblingIndex() == 1) {
                slider.value = GameControl.musicVolume;
                slider.onValueChanged.AddListener(SetMusicVolume);
                SetMusicVolume(slider.value);
            }
        }
    }

    void SetSFXVolume(float value) {
        GameControl.sfxVolume = value;
    }

    void SetMusicVolume(float value) {
        GameControl.musicVolume = value;
    }

    public void DisableMenu(RectMask2D menu) {
        menu.gameObject.SetActive(false);
    }

    public bool IsActive(RectMask2D menu) {
        return menu.gameObject.activeSelf;
    }

    public void DisableMenuButtons(RectMask2D menu) {
        foreach (Button btn in menu.GetComponentsInChildren<Button>()) {
            btn.interactable = false;
        }
    }

    public void EnableMenuButtons(RectMask2D menu) {
        foreach (Button btn in menu.GetComponentsInChildren<Button>()) {
            btn.interactable = true;
        }
    }
}
