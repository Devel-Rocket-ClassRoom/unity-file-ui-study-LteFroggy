using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiInventorySlot : MonoBehaviour {
	[SerializeField] private Image _itemIcon;
	[SerializeField] private TextMeshProUGUI _itemName;
	private Button _button;
	
	public int SlotIdx { get; set; }
	
	public SaveItemData SaveItemData { get; private set; }
	
	private static Sprite EmptyImage => Resources.Load<Sprite>($"Icon/grey_crossWhite");

	private void Awake() {
		_button = GetComponent<Button>();
	}

	public void SetEmpty() {
		_itemIcon.sprite = EmptyImage;
		_itemName.text = string.Empty;
		SaveItemData = null;
	}
	
	public void AddToButton(UnityAction act) {
		_button.onClick.AddListener(act);
	}
	
	public void SetItem(SaveItemData data) {
		SaveItemData = data;
		_itemIcon.sprite = data.ItemData.SpriteIcon;
		_itemName.text = data.ItemData.StringName;
	}
}
