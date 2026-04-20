using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTable : DataTable {
	private readonly Dictionary<string, ItemData> _table = new Dictionary<string, ItemData>();
	
	public ItemData RandomData => _table.ElementAt(Random.Range(0, _table.Count)).Value;
	
	private List<string> _keyList;
	
	public override void Load(string fileName) {
		_table.Clear();
		
		string path = string.Format(FormatPath, fileName);
		
		Debug.Log($"ItemTable 초기화 시 파일 경로 : {path}");
		
		TextAsset textAsset = Resources.Load<TextAsset>(path);
		List<ItemData> itemList = LoadCSV<ItemData>(textAsset.text);
		
		foreach (ItemData item in itemList) {
			if (!_table.ContainsKey(item.Id)) {
				_table.Add(item.Id, item);
			} else {
				Debug.LogError($"아이템 아이디 중복됨 : {item.Id}");
			}
		}
		
		_keyList = _table.Keys.ToList();
	}
	
	public ItemData Get(string id) {
		if (_table.TryGetValue(id, out var value)) {
			return value;
		} else {
			Debug.LogError($"테이블에 존재하지 않는 아이템 조회 : {id}");
			return null;
		}
	}
	
	public ItemData GetRandomItem() {
		return _table[_keyList[Random.Range(0, _keyList.Count)]];
	}
}
