
public class Settings : Singleton<Settings>
{
    public Localization.Language Language;

    private Localization.Language currentLanguage;

    public static void UpdateLanguage(Localization.Language newLanguage)
    {
        Localization.LoadLanguage(newLanguage);
        Instance.currentLanguage = newLanguage;
    }

    void Start()
    {
        UpdateLanguage(Language);
    }

    void Update()
    {
        if (currentLanguage != Language)
        {
            UpdateLanguage(Language);
        }
    }
}
