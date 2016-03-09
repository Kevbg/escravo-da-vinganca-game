using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;


public class ExampleJson : MonoBehaviour {
	private string jsonString;

	// Use this for initialization
	void Start () {
		jsonString = File.ReadAllText (Application.dataPath + "/Json/String.json");

		Debug.Log (jsonString);

		JsonData jsonObject = JsonMapper.ToObject(jsonString);

		Debug.Log (jsonObject ["menu"] ["text_1"] ["portuguese"]);
		Debug.Log (jsonObject ["level_1"] ["text_1"] ["english"]);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
