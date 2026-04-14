using System;
using UnityEngine;
using UnityEngine.UI;

public class DataTableTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Language = Languages.Korean;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Language = Languages.English;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Language = Languages.Japanese;
        }
    }
}