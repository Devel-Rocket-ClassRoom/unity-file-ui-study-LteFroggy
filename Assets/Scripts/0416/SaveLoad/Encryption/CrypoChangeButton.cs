using System;
using UnityEngine;
using UnityEngine.UI;

public class CrypoChangeButton : MonoBehaviour {
	private Button _crypoChangeButton;

	private void Awake() {
		_crypoChangeButton = GetComponent<Button>();
		_crypoChangeButton.onClick.AddListener(ToggleCryptoState);
	}

	private void ToggleCryptoState() {
		if (SaveDataManager.Mode == SaveMode.Encrypted) {
			SaveDataManager.Mode = SaveMode.Text;
			Debug.Log($"Text모드로 변경");
		} else if (SaveDataManager.Mode == SaveMode.Text) {
			SaveDataManager.Mode = SaveMode.Encrypted;
			Debug.Log($"Crypto모드로 변경");
		} else {
			throw new InvalidOperationException();
		}
		
	}
}