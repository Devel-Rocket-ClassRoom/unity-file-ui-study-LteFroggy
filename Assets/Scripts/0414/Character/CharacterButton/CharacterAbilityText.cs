using System;
using TMPro;
using UnityEngine;

public class CharacterAbilityText : MonoBehaviour {
	private TextMeshProUGUI _abilityText;

	private void Awake() {
		_abilityText = GetComponent<TextMeshProUGUI>();
	}
	
	public void SetAbilityText(string text) {
		_abilityText.text = text;
	}
}