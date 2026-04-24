using UnityEngine;

public class UiInventorySlotInEquip : UiInventorySlot {
	[SerializeField] GameObject _equippingPanel;

	public override void SetEmpty() {
		base.SetEmpty();
		
		_equippingPanel.SetActive(false);
	}

	public override void SetItem(SaveItemData data) {
		base.SetItem(data);
		
		if (SaveItemData.EquippingCharacter != null) {
			_equippingPanel.SetActive(true);
		}
	}
}