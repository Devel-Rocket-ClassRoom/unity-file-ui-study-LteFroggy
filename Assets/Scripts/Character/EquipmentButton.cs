using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour {
	[SerializeField] private Image _itemImage;
	[SerializeField] private TextMeshProUGUI _itemName;
	private LocalizationText _nameLocalizationText;
	private Sprite _emptySprite;
	
	public void Init() {
		_emptySprite = Resources.Load<Sprite>($"Icons/grey_crossWhite");
		_nameLocalizationText = _itemName.GetComponent<LocalizationText>();
	}

	public void SetEmpty() {
		_itemImage.sprite = _emptySprite;
		// LocalizationText 비활성화하고 empty 처리
		_nameLocalizationText.enabled = false;
		_itemName.text = string.Empty;
	}
	
	public void SetItemData(SaveItemData data) {
		if (data == null) {
			SetEmpty();
			return;
		}
		
		_itemImage.sprite = data.ItemData.SpriteIcon;
		
		// id 설정하고 활성화
		_itemName.GetComponent<LocalizationText>().id = data.ItemData.Name;
		_nameLocalizationText.enabled = true;
	}
}