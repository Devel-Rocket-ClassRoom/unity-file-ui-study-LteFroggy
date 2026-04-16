using System;
using UnityEngine;

public class WindowManager : MonoBehaviour {
	[SerializeField] private GenericWindow[] _windows;
	private int _currentWindowId;
	private int _defaultWindowId = 0;

	private void Awake() {
		_currentWindowId = _defaultWindowId;
		Open(_currentWindowId);
		
		foreach (var window in _windows) {
			window.Init(this);
		}
	}

	public GenericWindow Open(int id) {
		CloseAllWindows();
		_currentWindowId = id;
		_windows[_currentWindowId].Open();
		
		return _windows[_currentWindowId];
	}
	
	private void CloseAllWindows() {
		foreach (var window in _windows) {
			window.gameObject.SetActive(false);
		}
	}
}
