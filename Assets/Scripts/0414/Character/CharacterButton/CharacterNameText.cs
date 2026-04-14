using TMPro;
using UnityEngine;

public class CharacterNameText : MonoBehaviour {
	private TextMeshProUGUI _nameText;

	private void Awake() {
		_nameText = GetComponent<TextMeshProUGUI>();
	}

	public void SetNameText(string text) {
		_nameText.text = text;
	}
}