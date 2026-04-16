using System;
using System.Collections.Generic;
using System.Text;

public class SaveDataV3 : SaveDataV2 {
	public override int Version => 3;
	public List<string> ItemList = new List<string>();
	
	public override SaveData VersionUp() {
		throw new NotImplementedException();
	}
	
	// SaveDataManager에서 만들 때
	public SaveDataV3() { }
	
	// SaveDataV2에서 VersionUp 할 때
	public SaveDataV3 (SaveDataV2 saveData) : base(saveData) { }

	public override string ToString() {
		StringBuilder sb = new StringBuilder();
		List<string> itemNameList = new List<string>();
		foreach (var itemId in ItemList) { itemNameList.Add(DataTableManager.StringTable.Get(DataTableManager.ItemTable.Get(itemId).Name)); }
		
		sb.Append($"{Name}의 소지금 : {Gold}\n");
		sb.Append($"소지품 리스트 : {string.Join(", ", itemNameList)}");
		
		return sb.ToString();
	}
}