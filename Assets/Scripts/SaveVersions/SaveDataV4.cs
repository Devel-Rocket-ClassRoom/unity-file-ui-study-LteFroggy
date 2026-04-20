using System.Collections.Generic;
using System.Text;

public class SaveDataV4 : SaveDataV3 {
	public override int Version => 4;
	public new List<SaveItemData> ItemList = new List<SaveItemData>();
	
	public override SaveData VersionUp() {
		throw new System.InvalidOperationException();
	}

	public override string ToString() {
		StringBuilder sb = new StringBuilder();
		List<string> itemNameList = new List<string>();
		foreach (var itemData in ItemList) { itemNameList.Add(DataTableManager.StringTable.Get(itemData.ItemData.Name)); }
		
		sb.Append($"{Name}의 소지금 : {Gold}\n");	
		sb.Append($"소지품 리스트 : {string.Join(", ", itemNameList)}");
		
		return sb.ToString();
	}
}