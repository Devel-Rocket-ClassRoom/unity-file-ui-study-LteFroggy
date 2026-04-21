using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCharacterSlot : MonoBehaviour {
	[SerializeField] private Image _characterImage;
	[SerializeField] private TextMeshProUGUI _characterJob;
	private LocalizationText _characterJobText;
	
	private Button _slotButton;
	private Sprite _emptySprite;
	public CharacterSaveData CharacterSaveData { get; set; }
	
	public int SlotNum { get; set; }
	
	public UnityEvent onSlotPressed;
	
	public void Init() {
		_emptySprite = Resources.Load<Sprite>($"Icons/grey_crossWhite");
		_slotButton = GetComponent<Button>();
		_characterJobText = _characterJob.gameObject.AddComponent<LocalizationText>();
		_characterJobText.id = string.Empty;
		_characterJobText.enabled = false;
		
		onSlotPressed = _slotButton.onClick;
	}

	public void SetEmpty() {
		_characterImage.sprite = _emptySprite;
		_characterJobText.enabled = false;
		_characterJob.text = string.Empty;
	}
	
	public void SetCharacterData(CharacterSaveData data) {
		CharacterSaveData = data;
		_characterJobText.id = data.CharacterData.Job;
		_characterJobText.enabled = true;
		_characterImage.sprite = data.CharacterData.SpriteIcon;
	}
}
