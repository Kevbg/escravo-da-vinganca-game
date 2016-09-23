using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {
    [Range(0, 255)]
    public float alpha = 100;
    public float fadeDuration = 0.2f;
    public GameObject pauseMenu;
    public GameObject pauseText;
    public GameObject gameOverMenu;
    private ScreenFader fader;
    private TextUpdater txtUpdater;

    void Start() {
        fader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        txtUpdater = GetComponent<TextUpdater>();
        txtUpdater.UpdateText(gameObject);
    }

    void Update() {
        if ((pauseMenu && !pauseMenu.activeSelf && Time.timeScale > 0) && Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        } else if ((pauseMenu && pauseMenu.activeSelf && Time.timeScale == 0) && Input.GetKeyDown(KeyCode.Escape)) {
            Resume();
        }
    }

    public void Pause() {
        Time.timeScale = 0;
        Show(pauseText);
        Show(pauseMenu);
        fader.FadeOut(fadeDuration, alpha);
    }

    public void Resume() {
        Time.timeScale = 1;
        Hide(pauseText);
        Hide(pauseMenu);
        fader.FadeIn(fadeDuration, 0);
    }

    public void GameOverPause() {
        Time.timeScale = 0;
        Show(gameOverMenu);
        fader.FadeOut(fadeDuration, alpha);
    }

    public void GameOverQuit() {
        Time.timeScale = 1;
        Hide(gameOverMenu);
        fader.FadeOut(fadeDuration, 0);
    }

    public void Hide(GameObject menu) {
        foreach (Image img in menu.GetComponentsInChildren<Image>()) {
            img.CrossFadeAlpha(0, fadeDuration, true);
        }

        foreach (Text txt in menu.GetComponentsInChildren<Text>()) {
            txt.CrossFadeAlpha(0, fadeDuration, true);
        }

        foreach (Button b in menu.GetComponentsInChildren<Button>()) {
            b.interactable = false;
        }

        StartCoroutine(TimedDisable(menu, fadeDuration));
    }

    public void Show(GameObject menu) {
        menu.SetActive(true);
        txtUpdater.UpdateText(menu);

        foreach (Image img in menu.GetComponentsInChildren<Image>()) {
            // Quando o obj é ativado os elementos voltam a ter alpha 1 automaticamente
            img.CrossFadeAlpha(0, 0, true);
            img.CrossFadeAlpha(1, fadeDuration, true);
        }

        foreach (Text txt in menu.GetComponentsInChildren<Text>()) {
            txt.CrossFadeAlpha(0, 0, true);
            txt.CrossFadeAlpha(1, fadeDuration, true);
        }

        foreach (Button b in menu.GetComponentsInChildren<Button>()) {
            b.interactable = true;
        }

        foreach (Slider s in menu.GetComponentsInChildren<Slider>()) {
           if (s.tag == "SFXSlider") {
                s.value = GameControl.sfxVolume;
                s.onValueChanged.AddListener(SetSFXVolume);
                SetSFXVolume(s.value);
            } else if (s.tag == "MusicSlider") {
                s.value = GameControl.musicVolume;
                s.onValueChanged.AddListener(SetMusicVolume);
                SetMusicVolume(s.value);
            }
        }
    }

    public IEnumerator TimedDisable(GameObject menu, float time) {
        yield return new WaitForSecondsRealtime(time);
        menu.SetActive(false);
    }

    void SetSFXVolume(float value) {
        GameControl.sfxVolume = value;
    }

    void SetMusicVolume(float value) {
        GameControl.musicVolume = value;
    }

    public void OpenWindow(GameObject window) {
        if (Time.timeScale > 0) {
            fader.FadeOut(fadeDuration, alpha);
        }
        window.SetActive(true);
        txtUpdater.UpdateText(window);
    }

    public void CloseWindow(GameObject window) {
        if (Time.timeScale > 0) {
            fader.FadeIn(fadeDuration, 0);
        }
        window.SetActive(false);
    }

    public void DisableSFXTriggers(GameObject menu) {
        foreach(Button b in menu.GetComponentsInChildren<Button>()) {
            b.GetComponent<EventTrigger>().enabled = false;
        }
    }

    public void EnableSFXTriggers(GameObject menu) {
        foreach (Button b in menu.GetComponentsInChildren<Button>()) {
            b.GetComponent<EventTrigger>().enabled = true;
        }
    }
}
