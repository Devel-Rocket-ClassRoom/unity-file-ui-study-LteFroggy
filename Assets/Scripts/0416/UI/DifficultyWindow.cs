using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow {
	[Header("=== 난이도 토글 등록 ===")]
	[SerializeField] private Toggle[] _diffToggles;

	[Header("=== 버튼 등록 ===")] 
	[SerializeField] private Button _cancelButton;
	[SerializeField] private Button _applyButton;
	
	private int _diffValue;

	public override void Open() {
		_diffValue = UISaveDataManager.Load();
		Debug.Log($"{_diffValue}");
		_firstSelected = _diffToggles[_diffValue].gameObject;
		
		base.Open();
		
		_diffToggles[0].onValueChanged.RemoveAllListeners();
		_diffToggles[0].onValueChanged.AddListener(OnEasyChanged);
		_diffToggles[1].onValueChanged.RemoveAllListeners();
		_diffToggles[1].onValueChanged.AddListener(OnNormalChanged);
		_diffToggles[2].onValueChanged.RemoveAllListeners();
		_diffToggles[2].onValueChanged.AddListener(OnHardChanged);
		
		_cancelButton.onClick.RemoveAllListeners();
		_cancelButton.onClick.AddListener(OnCancel);
		_applyButton.onClick.RemoveAllListeners();
		_applyButton.onClick.AddListener(OnApply);
	}

	public override void Close() {
		
		base.Close();
	}

	private void OnEasyChanged(bool value) {
		if (value) { _diffValue = 0; }
	}
	
	private void OnNormalChanged(bool value) {
		if (value) { _diffValue = 1; }
	}
	
	private void OnHardChanged(bool value) {
		if (value) { _diffValue = 2; }
	}
	
	private void OnCancel() {
		_windowManager.Open(WindowList.StartWindow);
	}
	
	private void OnApply() {
		UISaveDataManager.Save(_diffValue);
		
		_windowManager.Open(WindowList.StartWindow);
	}
}
