using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterPanel : MonoBehaviour {
	[SerializeField] private Button _saveButton;
	[SerializeField] private Button _loadButton;
	[SerializeField] private Button _addCharacterButton;
	[SerializeField] private Button _removeCharacterButton;

	[SerializeField] private TMP_Dropdown _filterDropdown;
	[SerializeField] private TMP_Dropdown _sortingDropdown;
	
	[SerializeField] private CharacterDetail _characterDetailPanel;
	
	private UiCharacterSlotList _slotList;
	
	// 버튼 내 텍스트에 LocalizationText 추가에 사용
	private readonly UnityAction<Button, string> AddLocalizationText = (btn, key) => {
		btn.GetComponentInChildren<TextMeshProUGUI>().AddComponent<LocalizationText>();
		btn.GetComponentInChildren<TextMeshProUGUI>().GetComponent<LocalizationText>().id = key;
	};
	
	// Dropdown에 추가하는 데에 사용
	private readonly UnityAction<TMP_Dropdown, string[]> AddLocalizationDropdown = (dropdown, keys) => {
		dropdown.AddComponent<LocalizationDropDown>();
		dropdown.GetComponent<LocalizationDropDown>().ids = keys;
	};

	private void Awake() {
		_slotList = GetComponentInChildren<UiCharacterSlotList>();
		
		// 디테일 패널 초기화
		_characterDetailPanel.Init();
		
		// 버튼별로 눌렀을 때의 이벤트 추가
		_saveButton.onClick.AddListener(OnSave);
		_loadButton.onClick.AddListener(OnLoad);
		_addCharacterButton.onClick.AddListener(_slotList.AddRandomCharacter);
		_removeCharacterButton.onClick.AddListener(_slotList.RemoveCharacter);
		_filterDropdown.onValueChanged.AddListener(OnFilterChanged);
		_sortingDropdown.onValueChanged.AddListener(OnSortingChanged);
		
		// 각 버튼에 컴포넌트 추가하기
		AddLocalizationText(_saveButton, "Save");
		AddLocalizationText(_loadButton, "Load");
		AddLocalizationText(_addCharacterButton, "AddCharacter");
		AddLocalizationText(_removeCharacterButton, "RemoveCharacter");
		
		// 캐릭터 슬롯 클릭 시 실행될 함수 추가
		_slotList.onSlotPressed.AddListener(UpdateDetailPanel);
		
		// Dropdown에도 추가
		List<string> options = new List<string>();
		for (int i = 0; i < Enum.GetValues(typeof(CharacterFilteringOption)).Length; i++) {
			options.Add(((CharacterFilteringOption)i).ToString());
		}
		AddLocalizationDropdown(_filterDropdown, options.ToArray());
		options.Clear();
		for (int i = 0; i < Enum.GetValues(typeof(CharacterSortingOption)).Length; i++) {
			options.Add(((CharacterSortingOption)i).ToString());
		}
		AddLocalizationDropdown(_sortingDropdown, options.ToArray());
		
		// 처음엔 디테일패널 없게
		_characterDetailPanel.gameObject.SetActive(false);
		
		// 로딩한 결과 적용
		_slotList.Init();
		OnLoad();
	}
	
	private void UpdateDetailPanel(CharacterSaveData data) {
		_characterDetailPanel.SetCharacterData(data);
		_characterDetailPanel.gameObject.SetActive(true);
	}

	private void OnDisable() {
		// _slotList.SaveData();
	}
	
	private void OnSave() {
		_slotList.SaveData();
	}
	
	private void OnLoad() {
		SaveDataManager.Load();
		_slotList.ApplySaveData();
		
		_filterDropdown.value = (int)_slotList.FilterOption;
		_sortingDropdown.value = (int)_slotList.SortingOption;
	}

	private void OnFilterChanged(int value) {
		_slotList.FilterOption = (CharacterFilteringOption)value;
	}
	
	private void OnSortingChanged(int value) {
		_slotList.SortingOption = (CharacterSortingOption)value;
	}
}
