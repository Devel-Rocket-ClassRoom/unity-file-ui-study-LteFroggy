using UnityEngine;

public class ItemInfoDetail : MonoBehaviour {
	private ItemNameText _name;
	private ItemDescText _desc;
    private ItemSpriteImage _itemSpriteImage;
    private ItemData _itemData;

    private void Awake() {
        _name = GetComponentInChildren<ItemNameText>();
        _desc = GetComponentInChildren<ItemDescText>();
        _itemSpriteImage = GetComponentInChildren<ItemSpriteImage>();
        
        Variables.OnLanguageChanged += UpdateItemData;
    }
    
    public void Initialize() {
	    if (_name == null) {
		    Debug.LogError("Name 컴포넌트가 지정되지 않았습니다");
		    return;
	    } if (_desc == null) {
		    Debug.LogError("Description 컴포넌트가 지정되지 않았습니다");
		    return;
	    } if (_itemSpriteImage == null) {
		    Debug.LogError("ItemSpriteImage 컴포넌트가 지정되지 않았습니다");
		    return;
	    }
	    
	    _name.SetItemNameText(string.Empty);		    
		_desc.SetItemDescText(string.Empty);
		_itemSpriteImage.SetSpriteImage(null);
    }

    // 자기 자신의 정보 갱신
	public void ChangeItemData(ItemData data) {
		_itemData = data;
		UpdateItemData();
	}
	
	// 실제 데이터 갱신
	private void UpdateItemData() {
		_name.SetItemNameText(_itemData.StringName);
		_desc.SetItemDescText(_itemData.StringDesc);
		_itemSpriteImage.SetSpriteImage(_itemData.SpriteIcon);
	}
}