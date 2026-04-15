using System;
using System.IO;
using UnityEngine;

public class JsonUtilityTest : MonoBehaviour {
	private string _savePath;
	private string _saveFileName;
	private string _saveFullPath;

	private void Initialize() {
		_savePath = Path.Combine(Application.persistentDataPath, "saves");
		_saveFileName = "player.json";
		_saveFullPath = Path.Combine(_savePath, _saveFileName);
	}

	private void Update() {
		if (_savePath == null) {
			Initialize();
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			// Save
			PlayerInfo obj = new PlayerInfo() {
				playerName = "홍길동",
				lives = 10,
				health = 10.999f,
				position = new Vector3(1f, 2f, 1f)
			};
			
			string saveFile = JsonUtility.ToJson(obj, prettyPrint : true);
			if (!Directory.Exists(_savePath)) { Directory.CreateDirectory(_savePath); }
			File.WriteAllText(_saveFullPath, saveFile);
			
			Debug.Log($"세이브파일 생성 완료 : {saveFile}");
		}
		
		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			if (!Directory.Exists(_savePath)) {
				Debug.LogError("세이브 파일 경로가 없습니다!");
			} else if (!File.Exists(_saveFullPath)) {
				Debug.LogError($"세이브 파일이 없습니다.");
			}
			
			string json = File.ReadAllText(_saveFullPath);
			
			// 함수 내부에서 new해서 새로운 객체 만들어 넘겨주기
			PlayerInfo obj = JsonUtility.FromJson<PlayerInfo>(json);
			// json 내용 기반으로 이미 만들어진 클래스에 값 덮어씌우기
			JsonUtility.FromJsonOverwrite(json, obj);
			
			Debug.Log(obj);
		} 
	}
}
