using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    public LanguageManager languageManager;
    // Start is called before the first frame update
    public void ChangeToSpanish()
    {
        languageManager.currentLanguage = Language.spa;
        languageManager.UpdateControls();
    }

    public void ChangeToEnglish()
    {
        languageManager.currentLanguage = Language.eng;
        languageManager.UpdateControls();
    }
}
