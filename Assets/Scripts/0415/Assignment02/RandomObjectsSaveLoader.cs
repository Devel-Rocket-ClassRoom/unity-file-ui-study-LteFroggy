using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.UI;

public class RandomObjectsSaveLoader : MonoBehaviour {
	private string _saveFolderPath;
	private string _saveFileName;
	private string _savePath;
	
	private JsonSerializerSettings _serializerSettings;
	private ObjectCreateDestroyer _objectCreateDestroyer;

	[Header("=== 세이브, 로드 버튼 ===")]
	[SerializeField] private Button _saveButton;
	[SerializeField] private Button _loadButton;

	private void Awake() {
		Initialize();
	}

	private void Initialize() {
		// 저장 경로 관련 초기화
		_saveFolderPath = Path.Combine(Application.persistentDataPath, "saves");
		_saveFileName = "randomObjects.json";
		_savePath = Path.Combine(_saveFolderPath, _saveFileName);
		
		// 생성 담당할 오브젝트 연결
		_objectCreateDestroyer = GameObject.FindWithTag(Tags.ObjectCreator).GetComponent<ObjectCreateDestroyer>();
		
		// 직렬화 세팅
		_serializerSettings = new JsonSerializerSettings();
		_serializerSettings.Converters.Add(new PosScaleConverter());
		_serializerSettings.Converters.Add(new QuaternionConverter());
		_serializerSettings.Converters.Add(new ColorConverter());
		_serializerSettings.Converters.Add(new StringEnumConverter());
		
		// 버튼에 함수 할당
		_saveButton.onClick.AddListener(Save);
		_loadButton.onClick.AddListener(Load);
	}
	
	private void Save() {
		if (_savePath == null) { Initialize(); }
		
		GameObject[] objects = GameObject.FindGameObjectsWithTag(Tags.SaveTarget);
		ObjectInfo[] randomObjectsArr = new ObjectInfo[objects.Length];
		
		for (int i = 0; i < randomObjectsArr.Length; i++) {
			randomObjectsArr[i] = objects[i].GetComponent<ObjectMeta>().SaveInfo;
		}
		
		string serializedString = JsonConvert.SerializeObject(randomObjectsArr, _serializerSettings);
		File.WriteAllText(_savePath, serializedString);
		
		Debug.Log($"총 {randomObjectsArr.Length}개의 도형 저장 완료");
	}
	
	private void Load() {
		// 존재하는 모든 도형 찾아서 삭제
		GameObject[] objects = GameObject.FindGameObjectsWithTag(Tags.SaveTarget);
		foreach (var obj in objects) { Destroy(obj); }
		
		Debug.Log($"총 {objects.Length}개의 도형 삭제 완료");
		
		// 로드
		string serializedString = File.ReadAllText(_savePath);
		ObjectInfo[] randomObjectsArr = JsonConvert.DeserializeObject<ObjectInfo[]>(serializedString, _serializerSettings);

		Debug.Log($"역직렬화 완료 : {randomObjectsArr.Length}");
		
		// 생성
		foreach (var obj in randomObjectsArr) {
			_objectCreateDestroyer.CreateObject(obj);
		}

		Debug.Log($"총 {randomObjectsArr.Length}개의 도형 로딩 완료");
	}
}