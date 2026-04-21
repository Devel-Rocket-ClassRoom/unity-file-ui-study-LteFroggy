using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlotList : MonoBehaviour {
	[SerializeField] private UiCharacterSlot _infoPrefab;
	private ScrollRect _scrollView;
	
	private List<CharacterSaveData> _saveList = new List<CharacterSaveData>();
	private readonly List<UiCharacterSlot> _slotList = new List<UiCharacterSlot>();

	[SerializeField] private int _selectedIdx;
	
	private CharacterFilteringOption _filterOption;
	private CharacterSortingOption _sortingOption;
	
	public UnityEvent<CharacterSaveData> onSlotPressed;
	
	public CharacterFilteringOption FilterOption {
		get => _filterOption;
		set {
			_filterOption = value;
			UpdateScrollView();
		}
	}
	
	public CharacterSortingOption SortingOption {
		get => _sortingOption;
		set {
			_sortingOption = value;
			UpdateScrollView();
		}
	}
	
	public void Init() {
		_scrollView = GetComponent<ScrollRect>();
		
		ApplySaveData();
	}
	
	public void ApplySaveData() {
		// 로딩된 옵션 적용
		_saveList = SaveDataManager.Data.CharacterList;
		FilterOption = SaveDataManager.Data.CharacterFilteringOption;
		SortingOption = SaveDataManager.Data.CharacterSortingOption;
		
		UpdateScrollView();
	}
	
	public void SaveData() {
		SaveDataManager.Data.CharacterFilteringOption = FilterOption;
		SaveDataManager.Data.CharacterSortingOption = SortingOption;
		SaveDataManager.Data.CharacterList = _saveList;
		
		SaveDataManager.Save();
	}
	
	public void AddRandomCharacter() {
		// 새로운 SaveCharacterData 만들기
		CharacterSaveData newCharacter = new CharacterSaveData(DataTableManager.CharacterTable.GetRandomCharacterData());
		_saveList.Add(newCharacter);
		
		UpdateScrollView();
	}
	
	// 필터링 옵션 가져오기
	private readonly Func<CharacterFilteringOption, Func<CharacterSaveData, bool>> GetFilterOption = (option) => option switch {
		CharacterFilteringOption.None => (x) => true,
		CharacterFilteringOption.Archer => (x) => x.CharacterData.Job == "Archer",
		CharacterFilteringOption.Defender => (x) => x.CharacterData.Job == "Defender",
		CharacterFilteringOption.Warrior => (x) => x.CharacterData.Job == "Warrior",
		CharacterFilteringOption.Magician => (x) => x.CharacterData.Job == "Magician",
		_ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
	};
	
	// 정렬 기준 가져오기
	private readonly Func<CharacterSortingOption, Comparison<CharacterSaveData>> GetSortingOption = (option) => option switch {
		CharacterSortingOption.CreatedAscending => (x, y) => x.CreatedTime.CompareTo(y.CreatedTime),		
		CharacterSortingOption.CreatedDescending => (x, y) => y.CreatedTime.CompareTo(x.CreatedTime),
		CharacterSortingOption.JobAscending => (x, y) => string.Compare(x.CharacterData.StringJob, y.CharacterData.StringJob, StringComparison.Ordinal),
		CharacterSortingOption.JobDescending => (x, y) => string.Compare(y.CharacterData.StringJob, x.CharacterData.StringJob, StringComparison.Ordinal),
		_ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
	};
	
	public void RemoveCharacter() {
		if (_selectedIdx == -1) {
			Debug.Log($"선택된 캐릭터가 없습니다!");
			return;
		}
		
		_saveList.Remove(_slotList[_selectedIdx].CharacterSaveData);
		_slotList[_selectedIdx].SetEmpty();
		_slotList[_selectedIdx].gameObject.SetActive(false);
	}
	
	// 새로운 사이즈에 맞게 ScrollView 내부의 콘텐츠 사이즈 갱신
	private void UpdateScrollView() {
		// 이번에 들어갈 값 필터링
		List<CharacterSaveData> dataToShow = _saveList.Where(GetFilterOption(FilterOption)).ToList();
		dataToShow.Sort(GetSortingOption(SortingOption));
		
		// 실제 들어갈 사이즈에 맞게 값 갱신
		if (_slotList.Count < dataToShow.Count) {
			for (int i = _slotList.Count; i < dataToShow.Count; i++) {
				// 새로운 Prefab으로 생성
				UiCharacterSlot newSlot = Instantiate(_infoPrefab, _scrollView.content);
				newSlot.Init();
				newSlot.SlotNum = i;
				newSlot.SetEmpty();
				newSlot.gameObject.SetActive(false);
				
				newSlot.onSlotPressed.AddListener(() => {
					_selectedIdx = newSlot.SlotNum;
					onSlotPressed?.Invoke(newSlot.CharacterSaveData);
				});
				
				_slotList.Add(newSlot);
			}
		}
		
		// 값에 맞게 데이터 삽입 후 활성화
		for (int i = 0; i < _slotList.Count; i++) {
			// 보여줄 데이터가 필요로 하는 공간까지는 값 할당
			if (i < dataToShow.Count) {
				_slotList[i].SetCharacterData(dataToShow[i]);
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
