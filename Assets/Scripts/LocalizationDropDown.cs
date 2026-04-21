using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class LocalizationDropDown : MonoBehaviour
{
#if UNITY_EDITOR
    // 플레이 모드가 아닐 때 인스펙터에서 미리보기용 언어
    public Languages editorLang;
#endif
    [SerializeField] private string[] _ids;
    // 드롭다운 옵션 텍스트를 가져올 StringTable 키 목록
    public string[] ids {
        get => _ids;
        set {
            _ids = value;
            if (_ids != null) {
                OnChangeLanguage();
            }
        }
    }
    // 로컬라이징 대상 TMP 드롭다운
    public TMP_Dropdown dropdown;

    private void Awake() {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            // 런타임에서는 언어 변경 이벤트를 구독해 즉시 반영
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangeLanguage();
        }
#if UNITY_EDITOR
        else
        {
            // 에디터에서는 선택된 editorLang 기준으로 미리보기
            OnChangeLanguage(editorLang);
        }
#endif
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            // 이벤트 구독 해제 (중복 호출/메모리 누수 방지)
            Variables.OnLanguageChanged -= OnChangeLanguage;
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
#endif
    }

    private void OnChangeLanguage()
    {
        // 현재 활성 언어 테이블 적용
        Apply(DataTableManager.StringTable);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        // 지정 언어 테이블을 가져와 미리보기 적용
        Apply(DataTableManager.GetStringTable(lang));
    }

    [ContextMenu("ChangeLanguage")]
    private void ChangeLanguage()
    {
        // 씬 내 모든 LocalizationDropDown에 동일 editorLang 동기화
        var all = FindObjectsByType<LocalizationDropDown>(FindObjectsSortMode.None);
        foreach (var item in all)
        {
            item.editorLang = editorLang;
            item.OnChangeLanguage(editorLang);
        }
    }
#endif

    private void Apply(StringTable table)
    {
        // 참조가 비어있으면 아무 작업도 하지 않음
        if (dropdown == null || ids == null)
            return;

        // 옵션 재생성 후에도 기존 선택 인덱스를 최대한 유지
        int prevValue = dropdown.value;
        dropdown.ClearOptions();

        var options = new List<TMP_Dropdown.OptionData>(ids.Length);
        for (int i = 0; i < ids.Length; i++)
            options.Add(new TMP_Dropdown.OptionData(table.Get(ids[i])));
        dropdown.AddOptions(options);

        if (ids.Length > 0)
            dropdown.value = Mathf.Clamp(prevValue, 0, ids.Length - 1);
        // 현재 value 기준으로 라벨/UI 갱신
        dropdown.RefreshShownValue();
    }
}
