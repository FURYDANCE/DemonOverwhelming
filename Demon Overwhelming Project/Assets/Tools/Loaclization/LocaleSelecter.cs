using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class LocaleSelecter : MonoBehaviour
{
    public bool Active;
    private void Start()
    {
        LoadLocale();
    }
    public void ChangeLocale(int localID)
    {
        if (Active == true)
            return;
        StartCoroutine(SelectLocale(localID));
    }
    IEnumerator SelectLocale(int localID)
    {
        Active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localID];
        Active = false;
    }
    public void LoadLocale()
    {
        int i = PlayerPrefs.GetInt(ConstantStrings.LanguageMode);
        if (i == 0)
            ChangeLocale(0);
        if (i == 1)
            ChangeLocale(1);
    }
}
