using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonTest1 : MonoBehaviour {
	private string _saveFolderPath;
	private string _saveFileName;
	private string _savePath;
	
	private Vector3Converter _vector3Converter;
	private JsonSerializerSettings _jsonSerializeSettings;
	
	private void Initialize() {
		_saveFolderPath = Path.Combine(Application.persistentDataPath, "saves");
		_saveFileName = "player.json";
		_savePath = Path.Combine(_saveFolderPath, _saveFileName);
		_vector3Converter = new Vector3Converter();
		
		_jsonSerializeSettings = new JsonSerializerSettings();
		_jsonSerializeSettings.Converters.Add(_vector3Converter);
		_jsonSerializeSettings.Formatting = Formatting.Indented;
	}
	
	private void Update() {
		if (_savePath == null) { Initialize(); }
		
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			// Save
			PlayerState obj = new PlayerState() {
				playerName = "홍길동",
				lives = 10,
				health = 10.999f
			};
			
			string saveFile = JsonConvert.SerializeObject(obj, _jsonSerializeSettings);
			if (!Directory.Exists(_saveFolderPath)) { Directory.CreateDirectory(_saveFolderPath); }
			File.WriteAllText(_savePath, saveFile);
			
			Debug.Log($"세이브파일 생성 완료 : {saveFile}");
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			if (!Directory.Exists(_saveFolderPath)) { 
				Debug.LogError("세이브 파일 경로가 없습니다!");
				return;
			}
			
			if (!File.Exists(_savePath)) {
				Debug.LogError($"세이브 파일이 없습니다.");
				return;
			}
			
			string json = File.ReadAllText(_savePath);
			PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, _jsonSerializeSettings);
			
			// json 내용 기반으로 이미 만들어진 클래스에 값 덮어씌우기
			JsonUtility.FromJsonOverwrite(json, obj);
			
			Debug.Log(obj);
		} 
	}
}
