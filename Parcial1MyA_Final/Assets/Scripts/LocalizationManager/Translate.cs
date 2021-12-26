using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translate : MonoBehaviour
{
    public string ID;

    public LanguageManager manager;

    public Text myView;

    string _myText = "";

    // Start is called before the first frame update
    void Start()
    {
        _myText = myView.GetComponent<Text>().text;
        manager.OnUpdate += ChangeLanguage;
    }

    void ChangeLanguage()
    {
        myView.text = manager.GetTranslate(ID);
    }
}
