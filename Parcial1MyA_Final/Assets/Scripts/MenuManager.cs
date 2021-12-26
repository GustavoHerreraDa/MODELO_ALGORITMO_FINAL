using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas creditsCanvas;
    public int menuState = 1;

    void Start()
    {
        menuState = 1;
    }

    void Update()
    {
        CheckMenuState();

    }
    public void CheckMenuState()
    {
        switch (menuState)
        {
            case 1:
                mainMenuCanvas.enabled = true;
                creditsCanvas.enabled = false;
                break;
            case 2:
                mainMenuCanvas.enabled = false;
                creditsCanvas.enabled = true;
                
                break;
           
        }
    }

    public void OnClick(int state)
    {
        menuState = state;
    }
    public void DoExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        var gameManager = FindObjectOfType<LanguageManager>();
        DontDestroyOnLoad(gameManager);
        SceneManager.LoadScene(0);
    }
}
