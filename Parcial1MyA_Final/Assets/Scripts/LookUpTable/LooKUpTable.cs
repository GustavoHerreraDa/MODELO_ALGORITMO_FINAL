using System.Collections.Generic;
using UnityEngine;

public class LooKUpTable<T1, T2>
{
    public delegate T2 FactoryMethod(T1 keyToReturn);

    Dictionary<T1, T2> distionaryValues = new Dictionary<T1, T2>();

    FactoryMethod factoryMethod;

    public LooKUpTable(FactoryMethod newFactory)
    {
        factoryMethod = newFactory;
    }

    public T2 ReturnValue(T1 myKey)
    {
        if (distionaryValues.ContainsKey(myKey))
        {
            return distionaryValues[myKey];
        }
        else
        {
            var value = factoryMethod(myKey);
            distionaryValues[myKey] = value;
            //Debug.Log("mykey " + myKey + " " + value);
            return value;
        }

    }
}
