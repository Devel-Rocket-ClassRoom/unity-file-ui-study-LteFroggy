using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 1. CSV 파일 만들기 (ID, 이름, 설명, 공격력 등등 해서 만들기)
// 2. DataTable 상속
// 3. DataTableManager 등록
// 4. 테스트 패널

public class CharacterTable : DataTable {
	private readonly Dictionary<string, CharacterData> _table = new();
	
	private List<string> _characterKeyList;
	
	public override void Load(string fileName) {
		_table.Clear();
		
		// 특정 경로의 파일 읽어오기
		string path = string.Format(FormatPath, fileName);
		
		Debug.Log($"CharacterTable 초기화 시 파일 경로 : {path}");
		
		TextAsset textAsset = Resources.Load<TextAsset>(path);
		List<CharacterData> characterList = LoadCSV<CharacterData>(textAsset.text);
		
		foreach (CharacterData character in characterList) {
			if (!_table.ContainsKey(character.Id)) {
				_table.Add(character.Id, character);
			} else {
				Debug.LogError($"캐릭터 아이디 중복됨 : {character.Id}");
			}
		}
		
		_characterKeyList = _table.Keys.ToList();
	}
	
	public CharacterData Get(string key) {
		if (_table.TryGetValue(key, out CharacterData data)) {
			return data;
		} else {
			Debug.LogError($"해당 키에 해당하는 캐릭터를 찾지 못했습니다.");
			return null;
		}
	}
	
	public CharacterData GetRandomCharacterData() {
		return _table[_characterKeyList[Random.Range(0, _characterKeyList.Count)]];
	}
}
