using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpriteImage : MonoBehaviour {
	private Image _spriteImage;

	private void Awake() {
		_spriteImage = GetComponent<Image>();
	}

	public void SetSpriteImage(Sprite sprite) {
		_spriteImage.sprite = sprite;
	}
}