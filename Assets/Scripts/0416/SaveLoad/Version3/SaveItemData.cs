using System;

[Serializable]
public class SaveItemData {
	public Guid InstanceId { get; set; }
	public ItemData ItemData { get; set; }
	public DateTime CreatedTime { get; set; }
	
	public SaveItemData(ItemData data) {
		InstanceId = Guid.NewGuid();
		CreatedTime = DateTime.Now;
		ItemData = data;
	}
	
	// ItemId만 들어오면 알아서 찾아다가 넣기
	public SaveItemData(string itemId) : this(DataTableManager.ItemTable.Get(itemId)) {}
}