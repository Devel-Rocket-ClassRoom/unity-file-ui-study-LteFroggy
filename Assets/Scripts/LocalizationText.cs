using TMPro;
using UnityEngine;

[ExecuteAlways]
public class LocalizationText : MonoBehaviour
{
#if UNITY_EDITOR
    public Languages editorLang;
#endif
    public string id;
    public TextMeshProUGUI text;

    private void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            OnChangedId();
            Variables.OnLanguageChanged += OnChangedId;
        }
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(editorLang);
        }
#endif
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangedId;
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(editorLang);
#endif
    }

    private void OnChangedId()
    {
        if (text == null) return;
        text.text = DataTableManager.StringTable.Get(id);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        if (text == null) return;
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(id);
    }

    [ContextMenu("ChangeAll")]
    private void ChangeAll()
    {
        LocalizationText[] texts = FindObjectsByType<LocalizationText>(FindObjectsSortMode.None);
        foreach (LocalizationText text in texts)
        {
            text.editorLang = editorLang;
            text.OnChangedId();
        }
    }
#endif
}