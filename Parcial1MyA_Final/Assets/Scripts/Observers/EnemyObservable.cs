using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObservable : MonoBehaviour, IObservable
{
    List<IObserver> _allObserver = new List<IObserver>();

    //Deprecated
    void Start()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Enemy_Destroy, Event_Enemy_Destroy);
    }

    void Event_Enemy_Destroy(params object[] parameterContainer)
    {
        Debug.Log("Event_Enemy_Destroy");
    }

    public void NotifyToObservers(string action)
    {
        for (int i = _allObserver.Count - 1; i >= 0; i--)
        {
            _allObserver[i].Notify(action);
        }
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObserver.Contains(obs))
        {
            _allObserver.Add(obs);
        }
    }

    public void Unsubscribe(IObserver obs)
    {
        if (_allObserver.Contains(obs))
        {
            _allObserver.Remove(obs);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NotifyToObservers("Event_Enemy_Destroy");
    }
}
