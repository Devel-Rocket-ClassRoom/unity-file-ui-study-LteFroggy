using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour {
	[SerializeField] private UiCharacterSlot _infoPrefab;
	private ScrollRect _scrollView;
	
	private readonly List<CharacterSaveData> _saveList = new List<CharacterSaveData>();
	private readonly List<UiCharacterSlot> _slotList = new List<UiCharacterSlot>();
	
	private int _selectedIdx;
	
	private CharacterFilteringOption _filterOption;
	private CharacterSortingOption _sortingOption;

	private void Awake() {
		_scrollView = GetComponent<ScrollRect>();
	}
	
	public void AddRandomCharacter() {
		// 새로운 SaveCharacterData 만들기
		CharacterSaveData newCharacter = new CharacterSaveData(DataTableManager.CharacterTable.GetRandomCharacterData());
		_saveList.Add(newCharacter);
		
		UpdateScrollView();
	}
	
	public void RemoveCharacter() {
		if (_selectedIdx == -1) {
			Debug.Log($"선택된 캐릭터가 없습니다!");
			return;
		}
		
		_saveList.Remove(_saveList[_selectedIdx]);
		_slotList[_selectedIdx].SetEmpty();
		_slotList[_selectedIdx].gameObject.SetActive(false);
	}
	
	// 새로운 사이즈에 맞게 ScrollView 내부의 콘텐츠 사이즈 갱신
	private void UpdateScrollView() {
		// 실제 들어갈 사이즈에 맞게 값 갱신
		if (_slotList.Count < _saveList.Count) {
			for (int i = _slotList.Count; i < _saveList.Count; i++) {
				// 새로운 Prefab으로 생성
				UiCharacterSlot newSlot = Instantiate(_infoPrefab, _scrollView.content);
				newSlot.SlotNum = i;
				newSlot.SetEmpty();
				newSlot.gameObject.SetActive(false);
				
				newSlot.onSlotPressed.AddListener(() => {
					_selectedIdx = newSlot.SlotNum;
				});
				
				_slotList.Add(newSlot);
			}
		}
		
		// 값에 맞게 데이터 삽입 후 활성화
		for (int i = 0; i < _slotList.Count; i++) {
			// saveList가 필요로 하는 공간까지는 사용
			if (i < _saveList.Count) {
				_slotList[i].SetCharacterData(_saveList[i]);
				_slotList[i].gameObject.SetActive(true);
			} else {
				_slotList[i].SetEmpty();
				_slotList[i].gameObject.SetActive(false);
			}
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Keypad2)) {
			AddRandomCharacter();
		}
	}
}
