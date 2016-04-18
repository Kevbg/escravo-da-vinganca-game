using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System;

public class JsonParser : MonoBehaviour {
    private string strings;
    private string stringsFilePath;
    private JsonData data;

	void Start () {
        // Verificar se o arquivo está no local correto após build
        // Roda no editor, mas parece que não é copiado para a pasta do executável
        stringsFilePath = Application.dataPath + "/Json/Strings.json";
        Parse(stringsFilePath);
    }

    void Parse(string filePath) {
        strings = File.ReadAllText(stringsFilePath);
        data = JsonMapper.ToObject(strings);
    }

    public JsonData GetData(string scene, string item, string language) {
        return data[scene][item][language];
    }

	public void onGui() {
		GUI.Label (new Rect (10, 10, 200, 200), stringsFilePath);
	}
}
