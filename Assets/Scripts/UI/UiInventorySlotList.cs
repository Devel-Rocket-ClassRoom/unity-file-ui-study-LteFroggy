using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiInventorySlotList : MonoBehaviour {
	[SerializeField] private UiInventorySlot _itemSlotPrefab;
	[SerializeField] private ScrollRect _scrollRect;
	
	private int _selectedSlotIdx;
	
	private readonly List<UiInventorySlot> _itemSlots = new List<UiInventorySlot>();
	private List<SaveItemData> _tempItemList;
	
	[HideInInspector] public UnityEvent<SaveItemData> onSelectedSlot;
	[HideInInspector] public UnityEvent onRemoveSlot;
	
	private InventoryFilteringOption _filterOption;
	private InventorySortingOption _sortingOption;

	public InventoryFilteringOption FilterOption {
		get => _filterOption;
		set {
			_filterOption = value;
			UpdateSlots();
		}
	}
	
	public InventorySortingOption SortingOption {
		get => _sortingOption;
		set {
			_sortingOption = value;
			UpdateSlots();
		}
	}
	
	public List<SaveItemData> TempItemList {
		get => _tempItemList;
		set => _tempItemList = value.ToList();
	}
	
	private readonly Func<InventoryFilteringOption, Func<SaveItemData, bool>> GetFilteringMethod = (option) => option switch {
		InventoryFilteringOption.None => (x) => true,
		InventoryFilteringOption.Weapon => (x) => x.ItemData.Type == ItemTypes.Weapon,
		InventoryFilteringOption.Equip => (x) => x.ItemData.Type == ItemTypes.Equip,
		InventoryFilteringOption.Consumable => (x) => x.ItemData.Type == ItemTypes.Consumable,
		InventoryFilteringOption.NotConsumable => (x) => x.ItemData.Type != ItemTypes.Consumable,
		_ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
	};
	
	private readonly Func<InventorySortingOption, Comparison<SaveItemData>> GetSortingMethod = (option) => option switch {
		InventorySortingOption.CreatedAscending => (x, y) => y.CreatedTime.CompareTo(x.CreatedTime),
		InventorySortingOption.CreatedDescending => (x, y) => x.CreatedTime.CompareTo(y.CreatedTime),
		InventorySortingOption.NameAscending => (x, y) => string.Compare(y.ItemData.StringName, x.ItemData.StringName, StringComparison.Ordinal),
		InventorySortingOption.NameDescending => (x, y) => string.Compare(x.ItemData.StringName, y.ItemData.StringName, StringComparison.Ordinal),
		InventorySortingOption.TypeAscending => (x, y) => y.ItemData.Type.CompareTo(x.ItemData.Type),
		InventorySortingOption.TypeDescending => (x, y) => x.ItemData.Type.CompareTo(y.ItemData.Type),
		InventorySortingOption.CostAscending => (x, y) => y.ItemData.Cost.CompareTo(x.ItemData.Cost),
		InventorySortingOption.CostDescending => (x, y) => x.ItemData.Cost.CompareTo(y.ItemData.Cost),
		_ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
	};
		
	// 초기화
	public void Init() {
		TempItemList = SaveDataManager.Data.ItemList;
		
		// FilteringOption, SortingOption 적용
		FilterOption = SaveDataManager.Data.InventoryFilteringOption;
		SortingOption = SaveDataManager.Data.InventorySortingOption;
		
		UpdateSlots();
	}
	
	// 사라질 때 저장
	public void SetSaveData() {
		SaveDataManager.Data.ItemList = TempItemList;
		SaveDataManager.Data.filteringOption = FilterOption;
		SaveDataManager.Data.sortingOption = SortingOption;
	}
	
	public void UpdateSlots() {
		// 필터링
		List<SaveItemData> filteredList = _tempItemList.Where(GetFilteringMethod(FilterOption)).ToList();
		// 정렬
		filteredList.Sort(GetSortingMethod(SortingOption));
		
		// 현재 슬롯이 부족하면, 추가
		if (_itemSlots.Count < filteredList.Count) {
			for (int i = _itemSlots.Count; i < _tempItemList.Count; i++) {
				UiInventorySlot newSlot = Instantiate(_itemSlotPrefab, _scrollRect.content);
				newSlot.SlotIdx = i;
				newSlot.SetEmpty();
				newSlot.gameObject.SetActive(false);
				
				newSlot.AddToButton(() => {
					_selectedSlotIdx = newSlot.SlotIdx;
					onSelectedSlot?.Invoke(newSlot.SaveItemData);
				});
				
				_itemSlots.Add(newSlot);
			}
		}
		
		// 남으면, 필요한 만큼만 활성화하고 나머지는 비활성화
		for (int i = 0; i < _itemSlots.Count; i++) {
			if (i < filteredList.Count) {
				_itemSlots[i].SetItem(filteredList[i]);
				_itemSlots[i].gameObject.SetActive(true);
			}
			
			else {
				_itemSlots[i].SetEmpty();
				_itemSlots[i].gameObject.SetActive(false);
			}
		}
	}
	
	// 새 아이템 추가 후 업데이트
	public void AddRandomItem() {
		_tempItemList.Add(new SaveItemData(DataTableManager.ItemTable.GetRandomItem()));		
		
		UpdateSlots();
	}
	
	public void RemoveItem() {
		if (_selectedSlotIdx != -1) {
			_tempItemList.Remove(_itemSlots[_selectedSlotIdx].SaveItemData);
			_itemSlots[_selectedSlotIdx].gameObject.SetActive(false);
			_selectedSlotIdx = -1;
			
			onRemoveSlot?.Invoke();
			
			Debug.Log($"선택된 아이템을 제거하였습니다.");
		} else {
			Debug.Log($"선택된 아이템이 없습니다.");
		}
		
		UpdateSlots();
	}
}
