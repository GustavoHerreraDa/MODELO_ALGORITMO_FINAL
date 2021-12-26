using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Language
{
    eng,
    spa
}
public class LanguageManager : MonoBehaviour
{
    public Language currentLanguage;

    public Dictionary<Language, Dictionary<string, string>> Language_Manager;

    public string externalURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRRIoRWLur4tvH07G_LJvg5nZ_Ksa-LtECr_TJBLt98WjuljbBfziWAfaTTA6j70CeCq2sJ0XqegFop/pub?output=csv";

    public event Action OnUpdate = delegate { };
    void Awake()
    {
        StartCoroutine(DownloadCSV(externalURL));
        OnUpdate();
    }

    void Update()
    {


    }

    public void UpdateControls()
    {
        OnUpdate();
    }

    public string GetTranslate(string _id)
    {
        if (!Language_Manager[currentLanguage].ContainsKey(_id))
            return "Error 404: Not Found";
        else
            return Language_Manager[currentLanguage][_id];
    }

    public IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        Language_Manager = LanguageReader.loadCodexFromString("www", www.downloadHandler.text);

        OnUpdate();
    }

}
