using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInvenSlot : MonoBehaviour {
	[SerializeField] private Image _itemIcon;
	[SerializeField] private TextMeshProUGUI _itemName;
	
	public SaveItemData ItemData { get; private set; }
	
	private static Sprite EmptyImage => Resources.Load<Sprite>($"Icon/grey_crossWhite");

	private void SetEmpty() {
		_itemIcon.sprite = EmptyImage;
		_itemName.text = string.Empty;
		ItemData = null;
	}
	
	private void ToRandomData() {
		SetItem(DataTableManager.ItemTable.GetRandomItem());
	}
	
	public void SetItem(ItemData data) {
		_itemIcon.sprite = data.SpriteIcon;
		_itemName.text = data.StringName;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			ToRandomData();
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			SetEmpty();
		}
	}
}
