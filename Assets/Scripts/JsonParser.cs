using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System;

public class JsonParser : MonoBehaviour {
    private string strings;
    private string stringsFilePath;
    private JsonData data;

	void Awake () {
        stringsFilePath = Application.dataPath + "/StreamingAssets/Json/Strings.json";
        Parse(stringsFilePath);
    }

    void Parse(string filePath) {
        strings = File.ReadAllText(stringsFilePath);
        data = JsonMapper.ToObject(strings);
    }

    public JsonData GetItem(string item, string language) {
        return data[item][language];
    }

    public JsonData GetDialogue(int index, string language) {
        return data["Dialogue"][index][language];
    }

    public JsonData GetDialogue(int index, string language, out string character) {
        character = data["Dialogue"][index]["character"].ToString();
        return data["Dialogue"][index][language];
    }
}
