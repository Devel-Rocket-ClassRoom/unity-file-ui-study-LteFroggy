using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlotList : MonoBehaviour {
	[SerializeField] private UiInvenSlot _itemSlotPrefab;
	[SerializeField] private ScrollRect _scrollRect;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			for (int i = 0; i < 10; i++) {
				ItemData saveItemData = DataTableManager.ItemTable.GetRandomItem();
				UiInvenSlot item = Instantiate(_itemSlotPrefab, _scrollRect.content);
				item.SetItem(saveItemData);
			}			
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			
		}
	}
}
