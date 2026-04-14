using TMPro;
using UnityEngine;

public class ItemNameText : MonoBehaviour {
	private TextMeshProUGUI _nameText;

	private void Awake() {
		_nameText = GetComponent<TextMeshProUGUI>();
	}
	
	public void SetItemNameText(string nameText) {
		_nameText.text = nameText;
	}
}