using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.TextCore.Text;

// 특정 버전을 manager내에서 명시하지 않기 위해서 using 사용 -> 버전 수정 시 using만 수정
using SaveDataVC = SaveDataV5;

public static class SaveDataManager {
	// 해당 Client에서 사용하고 있는 SaveData의 Version
	public static int SaveDataVersion { get; } = 5;
	public static SaveDataVC Data { get; set; } = new SaveDataVC();
	
	public static SaveMode Mode { get; set; } = SaveMode.Text;
	
	private static readonly string saveFolderPath = Path.Combine(Application.persistentDataPath, "saves");
	private static readonly string[] saveFileNames = {
		"AutoSave",
		"SaveSlot1",
		"SaveSlot2",
		"SaveSlot3",
	};
	
	private static string Extension => Mode switch {
		SaveMode.Text => ".json",
		SaveMode.Encrypted => ".dat",
		_ => throw new ArgumentOutOfRangeException()
	};
	
	private static readonly JsonSerializerSettings settings = new JsonSerializerSettings() {
		Formatting = Formatting.Indented,
		TypeNameHandling = TypeNameHandling.All,
		Converters = {
			// 개발 중 Converter가 추가된다면, 여기에 추가하기
			new ItemDataConverter(),
			new CharacterDataConverter(),
		}
	};
	
	private static string GetSaveFilePath(int slotNum) {
		return Path.Combine(saveFolderPath, saveFileNames[slotNum] + Extension);
	}
	
	public static bool Save(int slotNum = 0) {
		// 데이터가 없거나, 유효하지 않은 슬롯 번호면 false
		if (Data == null || slotNum < 0 || slotNum >= saveFileNames.Length) { return false; }
		// 폴더 없다면 만들기
		if (!Directory.Exists(saveFolderPath)) { Directory.CreateDirectory(saveFolderPath); }
		
		try {
			string savePath = GetSaveFilePath(slotNum);
			string serializedString = JsonConvert.SerializeObject(Data, settings);
			
			switch (Mode) {
				case SaveMode.Text :
					File.WriteAllText(savePath, serializedString);
					break;
				case SaveMode.Encrypted : 
					byte[] encryptedBytes = CryptoUtil.Encrypt(serializedString);
					File.WriteAllBytes(savePath, encryptedBytes);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			Debug.Log($"{saveFileNames[slotNum]} 슬롯에 세이브 완료");
			
			return true;
		} catch {
			Debug.LogError("저장 안됨");
		}
		
		return false;
	}
	
	public static bool Load(int slotNum = 0) {
		// 유효하지 않은 슬롯 번호면 false
		if (slotNum < 0 || slotNum >= saveFileNames.Length) {
			Debug.LogError($"유효하지 않은 슬롯 번호입니다.");
			return false;
		}
		
		string savePath = GetSaveFilePath(slotNum);
		if (!File.Exists(savePath)) {
			return Save(slotNum);
		}
		
		// 로드할 경로 탐색
		try {
			string readedContent = Mode switch {
				SaveMode.Text => File.ReadAllText(savePath),
				SaveMode.Encrypted => CryptoUtil.Decrypt(File.ReadAllBytes(savePath)),
				_ => throw new InvalidOperationException()
			};

			SaveData data = JsonConvert.DeserializeObject<SaveData>(readedContent, settings);
			while (data.Version < SaveDataVersion) {
				data = data.VersionUp();				
			}
			
			Data = data as SaveDataVC;
			
			Debug.Log($"{saveFileNames[slotNum]}슬롯에서 로드 완료");
			return true;
		} catch (Exception e){
			Debug.LogError($"{e}");
		}
		return false;
	}
}