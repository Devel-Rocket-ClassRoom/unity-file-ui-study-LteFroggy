using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour {
	
	protected GameObject firstSelected;
	protected WindowManager _windowManager;
	
	public virtual void Open() {
		gameObject.SetActive(true);
		// 현재 Scene의 EventSystem 가져오기
		EventSystem.current.SetSelectedGameObject(firstSelected);
	}
	
	public void Init(WindowManager mgr) {
		_windowManager = mgr;
	}
	
	public virtual void Close() {
		gameObject.SetActive(false);
	}
}
