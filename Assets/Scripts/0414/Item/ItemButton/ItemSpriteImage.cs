using UnityEngine;
using UnityEngine.UI;

public class ItemSpriteImage : MonoBehaviour {
	private Image _image;

	private void Awake() {
		_image = GetComponent<Image>();
	}
	
	public void SetSpriteImage(Sprite sprite) {
		_image.sprite = sprite;
	}
}