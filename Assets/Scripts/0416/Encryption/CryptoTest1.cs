using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CryptoTest1 : MonoBehaviour {
	
	private byte[] encryptedData;
	
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			string plainText = "Hello My Name Is Hong Gil Dong";
			
			encryptedData = CryptoUtil.Encrypt(plainText);

			Debug.Log($"암호화 완료 : {encryptedData}");
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			Debug.Log($"{CryptoUtil.Decrypt(encryptedData)}");
		}
	}
}