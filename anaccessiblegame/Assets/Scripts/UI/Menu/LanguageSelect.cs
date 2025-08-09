using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSelect : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    LocalizationController localization;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        localization = LocalizationController.Instance;
        if (localization == null) { return; }

        // populate dropdown with locale names
        dropdown.ClearOptions();
        List<string> languageNames = new List<string>();

        foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
        {
            languageNames.Add(locale.Identifier.CultureInfo.NativeName);
        }
        dropdown.AddOptions(languageNames);


        if (LocalizationSettings.SelectedLocale != null)
        {
            // set dropdown value to selected locale index
            dropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        }
    }

    public void ChangeLanguage(int index)
    {
        localization.SetLocale(LocalizationSettings.AvailableLocales.Locales[index]);
    }

    public void NextLanguage()
    {
        // if at end of list, loop around to beginning of list
        if (dropdown.value >= dropdown.options.Count -1)
        {
            dropdown.value = 0;
        }
        else { dropdown.value++; }
    }

    public void PreviousLanguage()
    {
        // if at start of list, loop around to end of list
        if (dropdown.value <= 0)
        {
            dropdown.value = dropdown.options.Count - 1;
        }
        else { dropdown.value--; }
    }

}
