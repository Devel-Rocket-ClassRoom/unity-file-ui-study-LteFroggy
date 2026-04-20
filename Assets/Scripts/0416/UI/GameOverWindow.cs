using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameOverWindow : GenericWindow {
	
	private float _lerpElapsed;
	private float _lerpTargetScore;
	
	private readonly float _lerpDuration = 3.0f;
	private readonly float _statAppearInterval = 0.5f;
	
	private Coroutine _runningCoroutine;
	
	[Header("=== 다음으로 버튼 ===")]
	[SerializeField] private Button _nextButton;

	[Header("=== Stat 및 값 텍스트 각각 삽입 ===")]
	[SerializeField] private TextMeshProUGUI[] _statTexts;
	[SerializeField] private TextMeshProUGUI[] _scoreTexts;

	[Header("=== 총 점수 텍스트와 값 ===")]
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _scoreValue;


	public override void Open() {
		_firstSelected = _nextButton.gameObject;
		
		base.Open();
		
		// 오픈 당시에는 모두 값 없게 처리
		for (int i = 0; i < _statTexts.Length; i++) {
			_statTexts[i].gameObject.SetActive(false);
			_scoreTexts[i].gameObject.SetActive(false);
		}
		_scoreText.gameObject.SetActive(false);
		_scoreValue.gameObject.SetActive(false);
		
		// 목표 점수 랜덤하게 설정
		_lerpTargetScore = Random.Range(50000000, 99999999);
		
		if (_runningCoroutine != null) {  StopCoroutine(_runningCoroutine); }
		_lerpElapsed = 0;
		_runningCoroutine = StartCoroutine(CoOpenStats());
		
		// 버튼 할당
		_nextButton.onClick.RemoveAllListeners();
		_nextButton.onClick.AddListener(OnNext);
	}

	private IEnumerator CoOpenStats() {
		// 특정 초마다 하나씩 공개
		for (int i = 0; i < _statTexts.Length; i++) {
			_statTexts[i].gameObject.SetActive(true);
			_scoreTexts[i].gameObject.SetActive(true);
			
			// SetActive(true) 이후에 한 프레임 줘야 레이아웃 이상하게 잡히는 문제 줄어듦
			yield return null;
			
			// 각 점수는 랜덤하게
			_scoreTexts[i].text = (Random.Range(0, 99)).ToString("D2"); 
			
			yield return new WaitForSeconds(_statAppearInterval);
		}
		
		// 다 공개했으면, 총점 열기
		_scoreText.gameObject.SetActive(true);
		_scoreValue.gameObject.SetActive(true);
		
		// 코루틴 내부에서 값 끝까지 갱신
		while (_lerpElapsed < _lerpDuration) {
			_lerpElapsed += Time.deltaTime;
			_scoreValue.text = ((int)Mathf.Lerp(0, _lerpTargetScore, _lerpElapsed / _lerpDuration)).ToString("D9");
			
			yield return null;
		}
		
		_runningCoroutine = null;
	}
	
	public override void Close() {
		if (_runningCoroutine != null) { StopCoroutine(_runningCoroutine); }
		_runningCoroutine = null;
		
		_nextButton.onClick.RemoveListener(OnNext);
		
		base.Close();
	}
	
	private void OnNext() {
		_windowManager.Open(WindowList.StartWindow);
	}
}
