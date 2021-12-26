using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPool<T>
{
    public delegate T FactoryMethod();

    private FactoryMethod factoryMethod;

    private List<T> currentStock;
    private bool isDynamic;

    private Action<T> enableCallback;
    private Action<T> disableCallback;

    private List<T> stackOfObjects;

    public ObjectPool(FactoryMethod factoryMethod, Action<T> enableCallback, Action<T> disableCallback, int initialStock = 5, bool isDynamic = true)
    {
        this.factoryMethod = factoryMethod;
        this.enableCallback = enableCallback;
        this.disableCallback = disableCallback;
        this.isDynamic = isDynamic;

        stackOfObjects = new List<T>();  

        for (int i = 0; i < initialStock; i++)
        {
            var obj = factoryMethod();
            disableCallback(obj);
            stackOfObjects.Add(obj); 
        }
    }

    public T GetObject()
    {
        var result = default(T); 

        if (stackOfObjects.Count > 0)  
        {
            result = stackOfObjects[0];
            stackOfObjects.RemoveAt(0); 
        }
        else if (isDynamic)
        {
            result = factoryMethod();  
        }

        enableCallback(result); 
        return result;  
    }

    public void ReturnObject(T obj)
    {
        disableCallback(obj);
        stackOfObjects.Add(obj); 
    }
}
