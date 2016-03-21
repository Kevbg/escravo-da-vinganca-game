using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public class GameControl : MonoBehaviour {
    public static GameControl current;

    private string saveFilePath;

    // Variáveis globais (preferências)
    public static string language;

    void Awake() {
         // Permite que haja um único current GC
        if (current == null) {
            DontDestroyOnLoad(gameObject);
            current = this;

            saveFilePath = Application.persistentDataPath + "/Preferences.dat";
            current.Load();
        } else if (current != this){
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit() {
        current.Save();
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);
        Preferences prefs = new Preferences();

        // Passa as variáveis para a classe que será serializada
        prefs.language = language;

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
        } else {
            throw new FileNotFoundException("Could not load prefs file", saveFilePath);
        }
    }
}

// Classe usada para salvar/carregar as variáveis globais definidas acima
[Serializable]
class Preferences {
    internal string language;
}
