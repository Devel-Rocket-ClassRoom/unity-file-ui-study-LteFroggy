using UnityEngine;
using UnityEngine.UI;

public class StartWindow : GenericWindow {

	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _optionButton;
	[SerializeField] private bool _canContinue;
	
	public override void Open() {
		_continueButton.gameObject.SetActive(_canContinue);
		
		base.Open();
		
		if (!_canContinue) {
			_firstSelected = _newGameButton.gameObject;
		} else {
			_firstSelected = _continueButton.gameObject;
		}
		
		_continueButton.onClick.RemoveAllListeners();
		_continueButton.onClick.AddListener(OnContinue);
		_newGameButton.onClick.RemoveAllListeners();
		_newGameButton.onClick.AddListener(OnNewGame);
		_optionButton.onClick.RemoveAllListeners();
		_optionButton.onClick.AddListener(OnOption);
	}
	
	public override void Close() {
		
		base.Close();
	}
	
	private void OnContinue() {
		_windowManager.Open(WindowList.GameOverWindow);
	}
	
	private void OnNewGame() {
		_windowManager.Open(WindowList.KeyboardWindow);
	}
	
	private void OnOption() {
		_windowManager.Open(WindowList.DifficultyWindow);
	}
}
