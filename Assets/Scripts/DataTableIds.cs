using System.IO;
using UnityEditor.Search;

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };
    
    public static string String => StringTableIds[(int)Variables.Language];
    public static string Item => Path.Combine("Item", "ItemTable");
    public static string Character => Path.Combine("Character", "CharacterTable");
}