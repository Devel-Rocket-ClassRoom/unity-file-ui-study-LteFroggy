using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiPanelInventory : MonoBehaviour {
	[Header("=== 패널 내부에 존재하는 드롭다운, 슬롯 추가 ===")]
	[SerializeField] private TMP_Dropdown _filteringDropdown;
	[SerializeField] private TMP_Dropdown _sortingDropdown;
	
	[SerializeField] private UiInventorySlotList _slotList;
	
	[SerializeField] private Button _saveButton;
	[SerializeField] private Button _loadButton;
	[SerializeField] private Button _addItemButton;
	[SerializeField] private Button _removeItemButton;
	
	[SerializeField] private UiItemInfo _uiItemInfo;
	
	private readonly UnityAction<Button, string> BindLocalizationText = (x, y) => {
		x.GetComponentInChildren<TextMeshProUGUI>().AddComponent<LocalizationText>();
		x.GetComponentInChildren<TextMeshProUGUI>().GetComponent<LocalizationText>().id = y;
		x.gameObject.SetActive(false);
		x.gameObject.SetActive(true);
	};
	
	private readonly UnityAction<TMP_Dropdown, List<string>> BindLocalizationDropdown = (x, y) => {
		x.AddComponent<LocalizationDropDown>();
		x.GetComponent<LocalizationDropDown>().ids = y.ToArray();
		x.gameObject.SetActive(false);
		x.gameObject.SetActive(true);
	};

	private void Awake() {
		_filteringDropdown.onValueChanged.AddListener(OnFilteringChange);
		_sortingDropdown.onValueChanged.AddListener(OnSortingChange);
		_saveButton.onClick.AddListener(OnSave);
		_loadButton.onClick.AddListener(OnLoad);
		_addItemButton.onClick.AddListener(OnAddItem);
		_removeItemButton.onClick.AddListener(OnRemoveItem);
		
		BindLocalizationText(_saveButton, "Save");
		BindLocalizationText(_loadButton, "Load");
		BindLocalizationText(_addItemButton, "AddItem");
		BindLocalizationText(_removeItemButton, "RemoveItem");
		
		// 필터링 옵션, 소팅 옵션들 등록
		List<string> options = new List<string>();
		for (int i = 0; i < Enum.GetValues(typeof(InventoryFilteringOption)).Length; i++) {
			options.Add(((InventoryFilteringOption)i).ToString());
		}
		BindLocalizationDropdown(_filteringDropdown, options);
		
		options.Clear();
		for (int i = 0; i < Enum.GetValues(typeof(InventorySortingOption)).Length; i++) {
			options.Add(((InventorySortingOption)i).ToString());
		}
		BindLocalizationDropdown(_sortingDropdown, options);
		
		// 자식 이벤트에 나 추가
		_slotList.onSelectedSlot.AddListener(ChangeDetailPanel);
		_slotList.onRemoveSlot.AddListener(UnenableDetailPanel);
		
		// 처음 detail패널은 비활성화
		_uiItemInfo.gameObject.SetActive(false); 
	}
	
	private void OnEnable() {
		OnLoad();
	}

	private void OnDisable() {
		_slotList.SetSaveData();
	}
	
	private void OnFilteringChange(int value) {
		_slotList.FilterOption = (InventoryFilteringOption)value;
	}
	
	private void OnSortingChange(int value) {
		_slotList.SortingOption = (InventorySortingOption)value;
	}
	
	private void OnSave() {
		_slotList.SetSaveData();
		SaveDataManager.Save();
	}
	
	private void OnLoad() {
		SaveDataManager.Load();
		_slotList.Init();
		
		// 저장된 값 기반으로 현재 드롭다운 선택지도 변경
		_filteringDropdown.value = (int)_slotList.FilterOption;
		_sortingDropdown.value = (int)_slotList.SortingOption;
	}
	
	private void OnAddItem() {
		_slotList.AddRandomItem();
	}
	
	private void OnRemoveItem() {
		_slotList.RemoveItem();
	}
	
	private void ChangeDetailPanel(SaveItemData data) {
		_uiItemInfo.SetItem(data.ItemData);
		_uiItemInfo.gameObject.SetActive(true);
	}
	
	private void UnenableDetailPanel() {
		_uiItemInfo.SetEmpty();
		_uiItemInfo.gameObject.SetActive(false);
	}
}
