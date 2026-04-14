using UnityEngine;
using TMPro;

[ExecuteAlways]
public class StringTableText : MonoBehaviour
{
    public string id;
    public Languages language;
    public TextMeshProUGUI text;

    private void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        OnChangedId();
    }

    private void OnChangedId()
    {
        Variables.Language = language;
        text.text = DataTableManager.StringTable.Get(id);
    }

    public void ChangeLanguage(Languages lang)
    {
        language = lang;
    }
}