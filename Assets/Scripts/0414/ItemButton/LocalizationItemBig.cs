using UnityEngine;

[ExecuteAlways]
public class LocalizationItemBig : MonoBehaviour {
	private ItemNameText _name;
	private ItemDescText _desc;
    private ItemSpriteImage _itemSpriteImage;
    private ItemData _itemData;

    private void Awake() {
        _name = GetComponentInChildren<ItemNameText>();
        _desc = GetComponentInChildren<ItemDescText>();
        _itemSpriteImage = GetComponentInChildren<ItemSpriteImage>();
    }

    // 처음 시작 시에는 데이터 없게
    private void Start() {
	    _name.SetItemNameText(string.Empty);
	    _desc.SetItemDescText(string.Empty);
	    _itemSpriteImage.SetSpriteImage(null);
    }

    // 자기 자신의 정보 갱신
	public void UpdateItemData(ItemData data) {
		_itemData = data;
		RenewData();
	}
	
	// 실제 데이터 갱신
	private void RenewData() {
		_name.SetItemNameText(_itemData.StringName);
		_desc.SetItemDescText(_itemData.StringDesc);
		_itemSpriteImage.SetSpriteImage(_itemData.SpriteIcon);
	}
}