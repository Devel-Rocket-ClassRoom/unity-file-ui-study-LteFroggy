using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow {

	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _optionButton;
	[SerializeField] private bool _canContinue;
	
	public override void Open() {
		_continueButton.gameObject.SetActive(_canContinue);
		
		if (!_canContinue) {
			firstSelected = _newGameButton.gameObject;
		} else {
			firstSelected = _continueButton.gameObject;
		}
		
		base.Open();
	}
	
	public override void Close() {
		base.Close();
	}
	
	private void Start() {
        _continueButton.onClick.AddListener(OnContinue);
        _newGameButton.onClick.AddListener(OnNewGame);
        _optionButton.onClick.AddListener(OnOption);
    }
	
	private void OnContinue() {
		Debug.Log($"OnContinue()");
	}
	
	private void OnNewGame() {
		Debug.Log($"OnNewGame()");
	}
	
	private void OnOption() {
		Debug.Log($"OnOption()");
	}
}
