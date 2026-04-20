using UnityEngine;
using UnityEngine.UI;

public class ItemAddButton : MonoBehaviour {
	private Button _addButton;

	private void Awake() {
		_addButton = GetComponent<Button>();
		
		_addButton.onClick.AddListener(AddItem);
	}
	
	private void AddItem() {
		// 랜덤한 데이터 하나 뽑기
		ItemData data = DataTableManager.ItemTable.RandomData;
		
		// 데이터 넣기
		SaveDataManager.Data.ItemList.Add(new SaveItemData(data));
		
		Debug.Log($"{DataTableManager.StringTable.Get(data.Name)}이(가) 인벤토리에 추가됨");
	}
}