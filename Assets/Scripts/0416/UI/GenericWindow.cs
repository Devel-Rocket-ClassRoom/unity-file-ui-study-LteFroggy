using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour {
	
	protected GameObject _firstSelected;
	protected WindowManager _windowManager;
	
	public virtual void Open() {
		gameObject.SetActive(true);
		// 현재 Scene의 EventSystem 가져오기
		EventSystem.current.SetSelectedGameObject(_firstSelected);
	}
	
	public void Init(WindowManager mgr) {
		_windowManager = mgr;
	}
	
	public virtual void Close() {
		Debug.Log($"{GetType().Name} 클래스 Close 완료");
		gameObject.SetActive(false);
	}
}
