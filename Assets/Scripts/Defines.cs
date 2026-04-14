using UnityEngine.Events;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public enum ItemTypes {
		Weapon,
		Equip,
		Consumable
}

public static class Variables
{
    public static event UnityAction OnLanguageChanged;

    private static Languages language = Languages.Korean;
    public static Languages Language
    {
        get
        {
            return language;
        }
        set
        {
            if (language == value)
            {
                return;
            }
            language = value;
            DataTableManager.ChangeLanguage(language);
            OnLanguageChanged?.Invoke();
        }
    }
}