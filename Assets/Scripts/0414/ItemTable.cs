using System.Collections.Generic;
using UnityEngine;

public class ItemTable : DataTable {
	private readonly Dictionary<string, ItemData> _table = new Dictionary<string, ItemData>();
	
	public override void Load(string fileName) {
		_table.Clear();
		
		string path = string.Format(FormatPath, fileName);
		TextAsset textAsset = Resources.Load<TextAsset>(path);
		List<ItemData> itemList = LoadCSV<ItemData>(textAsset.text);
		
		foreach (ItemData item in itemList) {
			if (!_table.ContainsKey(item.Id)) {
				_table.Add(item.Id, item);
			} else {
				Debug.LogError($"아이템 아이디 중복됨 : {item.Id}");
			}
		}
	}
	
	public ItemData Get(string id) {
		if (_table.TryGetValue(id, out var value)) {
			return value;
		} else {
			Debug.LogError($"테이블에 존재하지 않는 아이템 조회 : {id}");
			return null;
		}
	}
}
