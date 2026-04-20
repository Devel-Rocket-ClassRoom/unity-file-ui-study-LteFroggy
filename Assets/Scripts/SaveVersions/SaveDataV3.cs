using System;
using System.Collections.Generic;
using System.Text;

public class SaveDataV3 : SaveDataV2 {
	public override int Version => 3;
	public List<string> ItemList = new List<string>();
	
	public override SaveData VersionUp() {
		SaveDataV4 v4 = new SaveDataV4();
		
		v4.Name = Name;
		v4.Gold = Gold;
		
		foreach (var item in ItemList) {
			v4.ItemList.Add(new SaveItemData(DataTableManager.ItemTable.Get(item)));
		}
		
		return v4;
	}
	
	// SaveDataManager에서 만들 때
	public SaveDataV3() { }
	
	// V2 -> V3 마이그레이션 용도
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