using System.IO;
using System.Net.Mime;
using Newtonsoft.Json;
using UnityEngine;

public static class UISaveDataManager {
	
	private static string _saveFolderPath = Path.Combine(Application.persistentDataPath, "saves");
	private static string _saveFileName = "diff.json";
	private static string _savePath = Path.Combine(_saveFolderPath, _saveFileName); 
	
	public static void Save(int diffNum) {
		// 폴더 없으면 만들기
		if (!Directory.Exists(_saveFolderPath)) { Directory.CreateDirectory(_saveFolderPath); }
		
		// 값을 string으로
		string serializedString = JsonConvert.SerializeObject(diffNum);
		// 저장
		File.WriteAllText(_savePath, serializedString);
	}
	
	public static int Load() {
		if (!Directory.Exists(_saveFolderPath)) {
			Debug.Log($"로드할 폴더가 존재하지 않습니다!");
			return 1;
		}
		if (!File.Exists(_savePath)) {
			Debug.Log($"로드할 파일이 존재하지 않습니다!");
			return 1;
		}
		
		string serializedString = File.ReadAllText(_savePath);
		return JsonConvert.DeserializeObject<int>(serializedString);
	}
}