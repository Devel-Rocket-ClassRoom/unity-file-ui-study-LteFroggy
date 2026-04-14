using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PanelSwitcher : MonoBehaviour {
	private Button _button;
	private GameObject _itemPanel;
	private GameObject _characterPanel;
	private bool _isItemPanelActivated = false;

	private void Awake() {
		_button = GetComponent<Button>();
		_itemPanel = GameObject.FindWithTag(Tags.ItemPanel);
		_characterPanel = GameObject.FindWithTag(Tags.CharacterPanel);
		_button.onClick.AddListener(SwitchPanelState);
	}

	private void Start() {
		SwitchPanelState();
	}

	private void SwitchPanelState() {
		Debug.Log($"판넬 변경 버튼 눌림!");
		_isItemPanelActivated = !_isItemPanelActivated;
		
		if (_isItemPanelActivated) {
			_itemPanel.SetActive(true);
			_itemPanel.GetComponentInChildren<ItemInfoDetail>().Initialize();
			_characterPanel.SetActive(false);
		} else {
			_itemPanel.SetActive(false);
			_characterPanel.SetActive(true);
			_characterPanel.GetComponentInChildren<CharacterInfoDetail>().Initialize();
		}
	}
	
}
