using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoSimple : MonoBehaviour
{
    private ItemNameText _itemNameText;
    private Button _button;
    private ItemSpriteImage _itemSpriteImage;
    private ItemData _itemData;
    
    private ItemInfoDetail _infoDetailItem;
    [Header("=== 표시할 아이템의 ID ===")]
    [SerializeField] private string _itemId;

    private void Awake() {
        _itemNameText = GetComponentInChildren<ItemNameText>();
        _itemSpriteImage = GetComponentInChildren<ItemSpriteImage>();
        _button = GetComponent<Button>();
        _infoDetailItem = GameObject.FindWithTag(Tags.ItemInfoDetail).GetComponent<ItemInfoDetail>();
        
        _button.onClick.AddListener(() => _infoDetailItem.ChangeItemData(_itemData));
        Variables.OnLanguageChanged += UpdateItemData;
    }
    
    private void UpdateItemData() {
        _itemData = DataTableManager.ItemTable.Get(_itemId);
        
        if (_itemData == null) { return; }
        
        if (_itemNameText == null)  { return; }
        _itemNameText.SetItemNameText(_itemData.StringName);
        
        if (_itemSpriteImage == null) { return; }
        _itemSpriteImage.SetSpriteImage(_itemData.SpriteIcon);
    }
    
    // 시작할 때 id에 해당하는 정보 불러오고, 적용
    private void Start() {
        UpdateItemData();
    }

    private void OnValidate() {
        UpdateItemData();
    }
}