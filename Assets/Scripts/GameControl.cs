using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameControl : MonoBehaviour {
    public static GameControl current;
    private string saveFilePath;
    public Scene scene { get; private set; }

    public enum Scenes {
        menu,
        cena1,
        poste,
        mansao,
        mansaoFim,
        languageSelection
    }

    // Variáveis globais (preferências)
    public static string language;
    public static float sfxVolume;
    public static float musicVolume;

    void Awake() {
         // Permite que haja um único current GC
        if (current == null) {
            DontDestroyOnLoad(gameObject);
            current = this;

            scene = SceneManager.GetActiveScene();
            saveFilePath = Application.persistentDataPath + "/Preferences.dat";
            current.Load();
        } else if (current != this){
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit() {
        current.Save();
    }

    void OnLevelWasLoaded() {
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
        } else {
            SceneManager.LoadScene(Scenes.languageSelection.ToString());
            throw new FileNotFoundException("Could not load prefs file", saveFilePath);
        }
    }
}

// Classe usada para salvar/carregar as variáveis globais definidas acima
[Serializable]
class Preferences {
    internal string language;
    internal float sfxVolume;
    internal float musicVolume;
}
