using System;
using UnityEngine;

// 실제로 캐릭터가 저장되는 데에 사용될 데이터
public class CharacterSaveData {
	public Guid InstanceId { get; set; }
	public CharacterData CharacterData { get; set; }
	public DateTime CreatedTime { get; set; }

	public CharacterSaveData(CharacterData data) {
		InstanceId = Guid.NewGuid();
		CharacterData = data;
		CreatedTime = DateTime.Now;
	}
	
	private SaveItemData _weapon;
	private SaveItemData _armor;
	private int _exp;
	
	public SaveItemData Weapon {
		get => _weapon;
		set {
			if (value.ItemData.Type != ItemTypes.Weapon) {
				Debug.Log($"무기만 장착할 수 있습니다.");
				return;
			}
			
			_weapon = value;
		}
	}
	
	public SaveItemData Armor {
		get => _armor;
		set {
			if (value.ItemData.Type != ItemTypes.Equip) {
				Debug.Log($"방어구만 장착할 수 있습니다.");
				return;
			}
			
			_weapon = value;
		}
	}
	
	
}