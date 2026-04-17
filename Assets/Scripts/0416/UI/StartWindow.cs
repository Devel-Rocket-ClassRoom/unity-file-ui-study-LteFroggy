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
			_firstSelected = _newGameButton.gameObject;
		} else {
			_firstSelected = _continueButton.gameObject;
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
		_windowManager.Open(WindowList.GameOverWindow);
	}
	
	private void OnNewGame() {
		_windowManager.Open(WindowList.KeyboardWindow);
	}
	
	private void OnOption() {
		_windowManager.Open(WindowList.DifficultyWindow);
	}
}
