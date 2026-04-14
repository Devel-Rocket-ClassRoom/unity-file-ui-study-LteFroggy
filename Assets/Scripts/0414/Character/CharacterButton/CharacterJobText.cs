using TMPro;
using UnityEngine;

public class CharacterJobText : MonoBehaviour {
	private TextMeshProUGUI _jobText;

	private void Awake() {
		_jobText = GetComponent<TextMeshProUGUI>();
	}

	public void SetJobText(string text) {
		_jobText.text = text;
	}
}