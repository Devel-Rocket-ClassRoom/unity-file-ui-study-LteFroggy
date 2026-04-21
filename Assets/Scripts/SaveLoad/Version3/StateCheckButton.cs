using UnityEngine;
using UnityEngine.UI;

public class StateCheckButton : MonoBehaviour {
	private Button _checkButton;

	private void Awake() {
		_checkButton = GetComponent<Button>();
		
		_checkButton.onClick.AddListener(CheckData);
	}
	
	private void CheckData() {
		// 세이브데이터 로그로 출력시키기
		Debug.Log(SaveDataManager.Data);
	}
}