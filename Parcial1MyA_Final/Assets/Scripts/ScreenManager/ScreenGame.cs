using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGame : IScreen
{
    Dictionary<Behaviour, bool> behaviours = new Dictionary<Behaviour, bool>();

    public Transform root;


    public ScreenGame(Transform root)
    {
        this.root = root;
    }

    public void Activate()
    {
       foreach(var keyValue in behaviours)
        {
            keyValue.Key.enabled = keyValue.Value;
        }

        behaviours.Clear();
    }

    public void Desactive()
    {
        foreach (var behaviour in root.GetComponentsInChildren<Behaviour>())
        {
            behaviours[behaviour] = behaviour.enabled;
            behaviour.enabled = false;
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Free()
    {
        GameObject.Destroy(root.gameObject);
        return "delete";
    }
}
