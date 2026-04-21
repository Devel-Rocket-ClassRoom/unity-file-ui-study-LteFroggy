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

	public int CalculatedAttack {
		get {
			if (Weapon == null) { return CharacterData.Attack; }
			// 검 장착시 공격력 10 
			if (Weapon.ItemData.Id == "Item1") { return CharacterData.Attack + 10; }
			// 활 장착시 공격력 5
			if (Weapon.ItemData.Id == "Item3") { return CharacterData.Attack + 5; }
			
			return CharacterData.Attack;
		}
	}
	
	public int CalculatedDefense {
		get {
			if (Armor == null) { return CharacterData.Defence; }
			// 방패 장착시 방어력 10
			if (Armor.ItemData.Id == "Item2") { return CharacterData.Defence + 10; }
			
			return CharacterData.Defence;
		}
	}
	
	public int CalculatedMagicAttack {
		get {
			if (Weapon == null) { return CharacterData.MagicAttack; }
			// 책 장착 시 마법공격력 10
			if (Weapon.ItemData.Id == "Item5") { return CharacterData.MagicAttack + 10; }
			
			return CharacterData.MagicAttack;
		}
	}

	public SaveItemData Weapon {
		get => _weapon;
		set {
			if (value == null) { } 
			else if (value.ItemData.Type != ItemTypes.Weapon) {
				Debug.Log($"무기만 장착할 수 있습니다.");
				return;
			}
			
			_weapon = value;
		}
	}
	
	public SaveItemData Armor {
		get => _armor;
		set {
			if (value == null) { }
			else if (value.ItemData.Type != ItemTypes.Equip) {
				Debug.Log($"방어구만 장착할 수 있습니다.");
				return;
			}
			
			_weapon = value;
		}
	}
}