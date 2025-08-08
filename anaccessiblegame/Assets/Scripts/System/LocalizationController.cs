using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationController : MonoBehaviour
{

    public static LocalizationController Instance;
    private void Awake()
    {
        // singleton code
        if (Instance == null) { Instance = this; }
        else if (Instance != this) { Destroy(this); }

        // preserve previously selected language
        if (PlayerPrefs.HasKey("Language"))
        {
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                // found selected language, apply it
                if (locale.LocaleName == PlayerPrefs.GetString("Language"))
                {
                    SetLocale(locale);
                    break;
                }
            }
        }
    }

    public void SetLocale(Locale newLocale)
    {
        // if locale is null or invalid, do nothing
        if (newLocale == null) { return; }
        else if (!LocalizationSettings.AvailableLocales.Locales.Contains(newLocale)) { return; }


        LocalizationSettings.SelectedLocale = newLocale;

        // save language selection for later loading
        PlayerPrefs.SetString("Language", newLocale.LocaleName);
    }
}
