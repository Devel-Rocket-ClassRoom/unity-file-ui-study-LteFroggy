using TMPro;
using UnityEngine;

[ExecuteAlways]
public class ItemDescText : MonoBehaviour {
	private TextMeshProUGUI _nameText;

	private void Awake() {
		_nameText = GetComponent<TextMeshProUGUI>();
	}
	
	public void SetItemDescText(string desc) {
		_nameText.text = desc;
	}
}