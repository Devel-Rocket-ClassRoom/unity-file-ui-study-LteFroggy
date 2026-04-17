using UnityEngine;

public class WindowManager : MonoBehaviour {
	private int _currentWindowId;
	private readonly int _defaultWindowId = 0;
	
	[Header("=== WindowManager의 Window 리스트 ===")]
	[SerializeField] private GenericWindow[] _windows;

	private void Awake() {
		_windows = new GenericWindow[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			_windows[i] = transform.GetChild(i).GetComponent<GenericWindow>();
		}
		
		_currentWindowId = _defaultWindowId;
		Open((WindowList)_currentWindowId);
		
		foreach (var window in _windows) {
			window.Init(this);
		}
	}

	public GenericWindow Open(WindowList id) {
		CloseAllWindows();
		_currentWindowId = (int)id;
		_windows[_currentWindowId].Open();
		
		return _windows[_currentWindowId];
	}
	
	private void CloseAllWindows() {
		foreach (var window in _windows) {
			window.gameObject.SetActive(false);
		}
	}
}
