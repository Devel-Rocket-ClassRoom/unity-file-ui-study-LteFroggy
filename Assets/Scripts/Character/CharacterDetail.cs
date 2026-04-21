using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetail : MonoBehaviour {
	[SerializeField] private Image _characterImage;
	[SerializeField] private TextMeshProUGUI _characterJob;
	[SerializeField] private TextMeshProUGUI _characterAttack;
	[SerializeField] private TextMeshProUGUI _characterDefense;
	[SerializeField] private TextMeshProUGUI _characterMagicAttack;

	[SerializeField] private EquipmentButton _weaponButton;
	[SerializeField] private EquipmentButton _armorButton;
	
	private LocalizationText _jobLocalizationText;
	
	public void Init() {
		_weaponButton.Init();
		_armorButton.Init();
		_jobLocalizationText = _characterJob.GetComponentInChildren<LocalizationText>();
	}

	public void SetEmpty() {
		_characterImage.sprite = null;
		// 비활성화 시 empty 처리
		_jobLocalizationText.enabled = false;
		_characterJob.text = string.Empty;
		_characterAttack.text = string.Empty;
		_characterDefense.text = string.Empty;
		_characterMagicAttack.text = string.Empty;
		_weaponButton.SetEmpty();
		_armorButton.SetEmpty();
	}
	
	public void SetCharacterData(CharacterSaveData data) {
		_characterImage.sprite = data.CharacterData.SpriteIcon;
		_jobLocalizationText.id = data.CharacterData.Job;
		_jobLocalizationText.enabled = true;
		_characterAttack.text = data.CalculatedAttack.ToString();
		_characterDefense.text = data.CalculatedDefense.ToString();
		_characterMagicAttack.text = data.CalculatedMagicAttack.ToString();
		_weaponButton.SetItemData(data.Weapon);
		_armorButton.SetItemData(data.Armor);
	} 
}
