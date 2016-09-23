using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameControl : MonoBehaviour {
    public static GameControl current;
    private string saveFilePath;
    public Scene scene { get; private set; }

    public enum Scenes {
        menu,
        historia,
        survival,
        poste,
        mansaoIntro,
        mansaoLuta,
        mansaoFim,
        languageSelection,
        logo
    }

    // Variáveis globais (preferências)
    public static string language;
    public static float sfxVolume;
    public static float musicVolume;
    public static List<KeyValuePair<string, int>> scores;

    void Awake() {
         // Permite que haja um único current GC
        if (current == null) {
            DontDestroyOnLoad(gameObject);
            current = this;

            scene = SceneManager.GetActiveScene();
            saveFilePath = Application.persistentDataPath + "/Preferences.dat";
            current.Load();

            SceneManager.sceneLoaded += OnSceneLoaded;

        } else if (current != this){
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit() {
        current.Save();
    }

    void OnSceneLoaded(Scene s, LoadSceneMode mode) {
        scene = SceneManager.GetActiveScene();
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);
        Preferences prefs = new Preferences();

        // Passa as variáveis para a classe que será serializada
        prefs.language = language;
        prefs.sfxVolume = sfxVolume;
        prefs.musicVolume = musicVolume;
        prefs.scores = scores;

        bf.Serialize(file, prefs);
        file.Close();
    }

    public void Load() {
        if (File.Exists(saveFilePath)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            Preferences prefs = (Preferences)bf.Deserialize(file);
            file.Close();

            // Recebe as variáveis da classe desserializada
            language = prefs.language;
            sfxVolume = prefs.sfxVolume;
            musicVolume = prefs.musicVolume;

            if (prefs.scores == null) {
                scores = new List<KeyValuePair<string, int>>();
            } else {
                scores = prefs.scores;
            }
        } else {
            SceneManager.LoadScene(Scenes.languageSelection.ToString());
            sfxVolume = 0.75f;
            musicVolume = 0.75f;
            scores = new List<KeyValuePair<string, int>>();
            //throw new FileNotFoundException("Could not load prefs file", saveFilePath);
        }
    }
}

// Classe usada para salvar/carregar as variáveis globais definidas acima
[Serializable]
class Preferences {
    internal string language;
    internal float sfxVolume;
    internal float musicVolume;
    internal List<KeyValuePair<string, int>> scores;
}
