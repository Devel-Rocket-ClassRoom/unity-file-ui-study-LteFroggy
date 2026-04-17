using System.Collections;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow {
	private string _realText = "";
	private readonly int _maxInputLength = 11;
	
	// 커서 깜빡임 관련 변수
	private Coroutine _coCursorBlinking;
	private bool _isUnderscoreAppeared;
	private bool _isBlinking = true;
	private readonly float _blinkingInterval = 0.4f;
	
	private Coroutine _coDeleteAll;
	private readonly float _disappearAnimationInterval = 0.05f; 

	[Header("=== 입력 결과가 출력될 텍스트 ===")] 
	[SerializeField] private TextMeshProUGUI _inputField;

	[Header("=== 키보드 버튼 모두 등록 ===")]
	[SerializeField] private Button[] _keyboardButtons;

	[Header("=== 하단 버튼 등록 ===")]
	[SerializeField] private Button _cancelButton;
	[SerializeField] private Button _deleteButton;
	[SerializeField] private Button _acceptButton;
	
	// _realText가 변할때마다 _inputField가 같이 변하도록 하기 위해 Property 추가
	private string RealText {
		get => _realText;
		set {
			_realText = value;
			// 이미 realText가 최대치면, 표시값에 _는 더하지 않음
			if (_realText.Length >= _maxInputLength) {
				_inputField.text = _realText;
				_isBlinking = false;
				_isUnderscoreAppeared = false;
			}
			// 최대가 아니라면, 뒤에 _ 더하기
			else {
				_inputField.text = _realText + "_";
				_isBlinking = true;
				_isUnderscoreAppeared = true;
			}
		}
	}
	
	public override void Open() {
		_firstSelected = _cancelButton.gameObject;
		base.Open();
		
		// 처음 열리면 값은 언더바로
		DeleteAllTexts();
		
		foreach (var button in _keyboardButtons) {
			button.onClick.AddListener(() => OnKeyboardClicked(button));
		}
		
		_cancelButton.onClick.AddListener(OnCancel);
		_deleteButton.onClick.AddListener(OnDelete);
		_acceptButton.onClick.AddListener(OnAccept);
		
		// 처음 열리면, 커서 깜빡임 시작
		if (_coCursorBlinking != null) { StopCoroutine(_coCursorBlinking); }
		_coCursorBlinking = StartCoroutine(CoCursorBlinking());
	}
	
	private IEnumerator CoCursorBlinking() {
		while (true) {
			// 깜빡이지 않아도 되면 아무것도 안하기
			if (!_isBlinking) {}
			else {
				// 지금 언더스코어 있으면, 지우기
				if (_isUnderscoreAppeared) {
					_inputField.text = _realText;
					_isUnderscoreAppeared = false;
				}
				// 없으면, 추가하기
				else {
					_inputField.text = _realText + "_";
					_isUnderscoreAppeared = true;
				}
			}
			// 일정 간격마다 반복
			yield return new WaitForSeconds(_blinkingInterval);
		}
	}
	
	private IEnumerator CoDeleteAll() {
		// 시작 시에 커서 깜빡임 
		if (_coCursorBlinking != null) { StopCoroutine(_coCursorBlinking); }
		_coCursorBlinking = null;
		
		// 글자 하나씩 제거
		while (_realText.Length > 0) {
			RealText = RealText.Substring(0, RealText.Length - 1);
			yield return new WaitForSeconds(_disappearAnimationInterval);
		}
		
		// 종료 후에 다시 커서 깜빡임
		_coCursorBlinking = StartCoroutine(CoCursorBlinking());
		_coDeleteAll = null;
	}
	
	private void OnCancel() {
		DeleteAllTexts();
	}
	
	private void OnDelete() {
		DeleteOneText();
	}
	
	private void OnAccept() {
		_windowManager.Open(WindowList.StartWindow);
	} 
	
	// 한 글자 제거
	private void DeleteOneText() {
		// 글자수 0글자면 아무것도 하지 않음
		if (_realText.Length <= 0) { return; }
		
		// 전체 클리어 애니메이션 중에는 추가 삭제 금지
		if (_coDeleteAll != null) { return; }
		RealText = RealText.Substring(0, RealText.Length - 1);
	}
	
	// 글자 모두 제거
	private void DeleteAllTexts() {
		// 글자수 0글자면 아무것도 하지 않음
		if (_realText.Length <= 0) { return; }
		
		// 이미 모두 제거중이면 아무 동작 하지 않음
		if (_coDeleteAll != null) { return; }
		_coDeleteAll = StartCoroutine(CoDeleteAll());
	}
	
	public override void Close() {
		base.Close();
		
		DeleteAllTexts();
		
		// 닫힐 때 커서 깜빡임 제거
		if (_coCursorBlinking != null) { StopCoroutine(_coCursorBlinking); }
		
		// 이벤트 제거
		foreach (var button in _keyboardButtons) {
			button.onClick.RemoveListener(() => OnKeyboardClicked(button));
		}
		_cancelButton.onClick.RemoveListener(OnCancel);
		_deleteButton.onClick.RemoveListener(OnDelete);
		_acceptButton.onClick.RemoveListener(OnAccept);
	}
	
	public void OnKeyboardClicked(Button button) {
		// 전체 클리어 애니메이션 중에는 글자 추가 금지
		if (_coDeleteAll != null) { return; }
		// 이미 최대 글자 이상이면 아무 동작 없음
		if (_realText.Length >=  _maxInputLength) { return; }
		
		// 버튼 텍스트 가져오고, 더하기. 0번만 가져오는 이유는 값 입력하다가 엔터 친 곳 있어서..
		char buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text[0];
		RealText += buttonText;
	}
}
