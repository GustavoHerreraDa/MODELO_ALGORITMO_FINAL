using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public Stack<IScreen> stackScreen;

    public string lastResult;

    public int countStack;

    // Start is called before the first frame update
    private void Awake()
    {
        stackScreen = new Stack<IScreen>();
    }


    public void Pop()
    {
        if (stackScreen.Count <= 1)
            return;

        lastResult = stackScreen.Pop().Free();

        if (stackScreen.Count > 0)
            stackScreen.Peek().Activate();
    }

    public void Push(IScreen screen)
    {

        if (stackScreen.Count > 0)
            stackScreen.Peek().Desactive();

        stackScreen.Push(screen);
        screen.Activate();
    }

    public void Push(string resource)
    {
        Debug.Log("PASA POR ACA");
        var go = Instantiate(Resources.Load<GameObject>(resource));
        Push(go.GetComponent<IScreen>());
    }
}
