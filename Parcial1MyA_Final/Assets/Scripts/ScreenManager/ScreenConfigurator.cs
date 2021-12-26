using UnityEngine;

public class ScreenConfigurator : MonoBehaviour
{
    public Transform mainGameCanvas;

    ScreenManager screenManager;

    public LanguageManager languageManager;

    bool showPause = false;
    void Start()
    {
        screenManager = GetComponent<ScreenManager>();
        screenManager.Push(new ScreenGame(mainGameCanvas));
        languageManager = FindObjectOfType<LanguageManager>();

        //Cuando se ejecuta la escena sin pasar por el menu
        if (languageManager is null)
        {
            languageManager = gameObject.AddComponent<LanguageManager>();
            Debug.Log("languageManager IS NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!showPause)
            {
                showPause = true;
                var canvasPause = Instantiate(Resources.Load<ScreenPause>("Pause"));

                canvasPause.btnPause.text = languageManager.GetTranslate(canvasPause.BTN_PAUSE_ID);
                canvasPause.pauseLabel.text = languageManager.GetTranslate(canvasPause.BTN_TXT_ID);

                screenManager.Push(canvasPause);
            }
            else
            {
                showPause = false;
                screenManager.Pop();
            }
        }
    }
}
