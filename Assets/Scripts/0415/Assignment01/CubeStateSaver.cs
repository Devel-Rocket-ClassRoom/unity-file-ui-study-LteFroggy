using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CubeStateSaver : MonoBehaviour {
	
	private JsonSerializerSettings _serializerSettings;
	
	private string _saveFolderPath;
	private string _saveFileName;
	private string _savePath;
	
	private GameObject _target;

	[SerializeField] private Button _saveButton;
	[SerializeField] private Button _loadButton;

	private void Awake() {
		Initialize();
	}
	
	private void Initialize() {
		_saveFolderPath = Path.Combine(Application.persistentDataPath, "saves");
		_saveFileName = "SomeClass.json";
		_savePath = Path.Combine(_saveFolderPath, _saveFileName);
		_target = GameObject.FindWithTag(Tags.SaveTarget);
		
		_saveButton.onClick.AddListener(Save);
		_loadButton.onClick.AddListener(Load);
		
		_serializerSettings = new JsonSerializerSettings();
		_serializerSettings.Converters.Add(new ColorConverter());
		_serializerSettings.Converters.Add(new PosScaleConverter());
		_serializerSettings.Converters.Add(new QuaternionConverter());
		_serializerSettings.Formatting = Formatting.Indented;
	}
	
	public void Save() {
		// 큐브의 상태 저장
		SomeClass some = new SomeClass();
		some.color = _target.GetComponent<Renderer>().material.color;
		some.pos = _target.transform.position;
		some.rot = _target.transform.rotation;
		some.scale = _target.transform.localScale;
		
		if (!Directory.Exists(_saveFolderPath)) {
			Directory.CreateDirectory(_saveFolderPath);
		}
		
		string saveSerialized = JsonConvert.SerializeObject(some, _serializerSettings);;
		File.WriteAllText(_savePath,  saveSerialized);
		
		Debug.Log($"SomeClass 저장 완료 : {saveSerialized}");
	}
	
	public void Load() {
		// 로드
		if (!Directory.Exists(_saveFolderPath)) {
			Debug.LogError("세이브 파일 경로가 존재하지 않습니다.");
			return;
		}
			
		if (!File.Exists(_savePath)) {
			Debug.LogError("세이브 파일이 존재하지 않습니다");
			return;
		}
			
		string serialized = File.ReadAllText(_savePath);
		SomeClass some = JsonConvert.DeserializeObject<SomeClass>(serialized, _serializerSettings);
			
		Debug.Log($"역직렬화 완료 : {some}");
		
		// 저장된 내용 적용
		_target.GetComponent<Renderer>().material.color = some.color;
		_target.transform.position = some.pos;
		_target.transform.rotation = some.rot;
		_target.transform.localScale = some.scale;
	}
}