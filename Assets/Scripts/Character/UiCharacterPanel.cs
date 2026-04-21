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
	
	private UiCharacterSlotList _slotList;
	
	// 버튼 내 텍스트에 LocalizationText 추가하는 것
	private readonly UnityAction<Button, string> AddLocalizationText = (btn, key) => {
		btn.GetComponentInChildren<TextMeshProUGUI>().AddComponent<LocalizationText>();
		btn.GetComponentInChildren<TextMeshProUGUI>().GetComponent<LocalizationText>().id = key;
	};
	
	private readonly UnityAction<TMP_Dropdown, string[]> AddLocalizationDropdown = (dropdown, keys) => {
		dropdown.AddComponent<LocalizationDropDown>();
		dropdown.GetComponent<LocalizationDropDown>().ids = keys;
	};

	private void Awake() {
		_slotList = GetComponentInChildren<UiCharacterSlotList>();
		
		// 버튼별로 눌렀을 때의 이벤트 추가
		_addCharacterButton.onClick.AddListener(_slotList.AddRandomCharacter);
		_removeCharacterButton.onClick.AddListener(_slotList.RemoveCharacter);
		
		// 각 버튼에 컴포넌트 추가하기
		AddLocalizationText(_saveButton, "Save");
		AddLocalizationText(_loadButton, "Load");
		AddLocalizationText(_addCharacterButton, "AddCharacter");
		AddLocalizationText(_removeCharacterButton, "RemoveCharacter");
		
		// Dropdown에도 추가
	}
}
