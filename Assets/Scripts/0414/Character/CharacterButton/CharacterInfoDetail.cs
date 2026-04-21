using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInfoDetail : MonoBehaviour {
	private CharacterSpriteImage _spriteImage;
	private CharacterNameText _nameText;
	private CharacterAbilityText _abilityText;
	private CharacterJobText _jobText;
	private CharacterData _data;

	private void Awake() {
		_spriteImage = GetComponentInChildren<CharacterSpriteImage>();
		_nameText = GetComponentInChildren<CharacterNameText>();
		_abilityText = GetComponentInChildren<CharacterAbilityText>();
		_jobText = GetComponentInChildren<CharacterJobText>();
		
		Variables.OnLanguageChanged += UpdateCharacterInfo;
	}

	// 시작 시엔 모두 초기화
	public void Initialize() {
		if (_spriteImage == null) {
			Debug.LogError("CharacterSpriteImage가 초기화되지 않았습니다");
			return;
		} if (_nameText == null) {
			Debug.LogError("NameText가 초기화되지 않았습니다");
			return;
		} if (_abilityText == null) {
			Debug.LogError("AbilityText가 초기화되지 않았습니다");
			return;
		}  if (_jobText == null) {
			Debug.LogError("JobText가 초기화되지 않았습니다");
			return;
		}
		_spriteImage.SetSpriteImage(null);
		_nameText.SetNameText(string.Empty);
		_abilityText.SetAbilityText(string.Empty);
		_jobText.SetJobText(string.Empty);
	}

	public void ChangeCharacterInfo(CharacterData data) {
		_data = data;
		UpdateCharacterInfo();
	}
	
	public void UpdateCharacterInfo() {
		_jobText.SetJobText(_data.StringJob);
		_spriteImage.SetSpriteImage(_data.SpriteIcon);
		_abilityText.SetAbilityText(_data.AbilityText);
	}
}