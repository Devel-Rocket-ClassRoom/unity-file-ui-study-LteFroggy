using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiItemInfo : MonoBehaviour {

	[SerializeField] private Image _itemImage;
	[SerializeField] private TextMeshProUGUI _name;
	[SerializeField] private TextMeshProUGUI _desc;
	[SerializeField] private TextMeshProUGUI _type;
	[SerializeField] private TextMeshProUGUI _value;
	[SerializeField] private TextMeshProUGUI _cost;
	
	private readonly string formattedString = "{0} : {1}";
	
	public void SetEmpty() {
		_itemImage.sprite = null;
		_name.text = string.Empty;
		_desc.text = string.Empty;
		_type.text = string.Empty;
		_value.text = string.Empty;
		_cost.text = string.Empty;
	}
	
	public void SetItem(ItemData data) {
		StringTable st =  DataTableManager.StringTable;
		
		_itemImage.sprite = data.SpriteIcon;
		_name.text = string.Format(formattedString, st.Get("Name"), data.StringName);
		_desc.text = string.Format(formattedString, st.Get("Desc"), data.StringDesc);
		_type.text = string.Format(formattedString, st.Get("Type"), data.StringType);
		_value.text = string.Format(formattedString, st.Get("Value"), data.Value.ToString());
		_cost.text = string.Format(formattedString, st.Get("Cost"), data.Cost.ToString());
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			SetEmpty();
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			SetItem(DataTableManager.ItemTable.GetRandomItem());
		}
	}
}
