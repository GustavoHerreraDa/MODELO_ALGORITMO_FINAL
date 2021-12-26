using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPause : MonoBehaviour, IScreen
{
    bool activate;

    string result;

    public ScreenManager screenManager;

    public string BTN_PAUSE_ID = "btnPause";
    public string BTN_TXT_ID = "txtPause";

    public Text btnPause;
    public Text pauseLabel; 

    //public LanguageManager languageManager;

    public void Awake()
    {
        screenManager = FindObjectOfType<ScreenManager>();
        //languageManager = FindObjectOfType<LanguageManager>();
           
    }

    public void Start()
    {
        //var objectsTranslate = GetComponentsInChildren<Translate>();

        //foreach (var aux in objectsTranslate)
        //    aux.manager = languageManager;

        //languageManager.UpdateControls();
    }

    public void OnReturn()
    {
        if (!activate) return;

        result = "Ok";

        screenManager.Pop();
    }


    public void Activate()
    {
        activate = true;
    }

    public void Desactive()
    {
        activate = false;
    }

    public string Free()
    {
        Destroy(gameObject);
        return result;
    }
}
