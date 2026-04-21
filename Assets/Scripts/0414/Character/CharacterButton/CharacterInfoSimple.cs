using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoSimple : MonoBehaviour {
	private CharacterSpriteImage _spriteImage;
	private CharacterNameText _nameText;
	private Button _button;
	private CharacterData _characterData;
	private CharacterInfoDetail _detailInfo;
	
	[Header("=== 표시할 캐릭터의 ID ===")]
    [SerializeField] private string _characterId;

	private void Awake() {
		_button = GetComponent<Button>();
		_spriteImage = GetComponentInChildren<CharacterSpriteImage>();
		_nameText = GetComponentInChildren<CharacterNameText>();
		_characterData = DataTableManager.CharacterTable.Get(_characterId);
		_detailInfo = GameObject.FindWithTag(Tags.CharacterInfoDetail).GetComponent<CharacterInfoDetail>();
		
		_button.onClick.AddListener(() => _detailInfo.ChangeCharacterInfo(_characterData));
		Variables.OnLanguageChanged += UpdateUI;
	}

	private void Start() {
		UpdateUI();
	}

	private void UpdateUI() {
		_spriteImage.SetSpriteImage(_characterData.SpriteIcon);
	}
	
}