using System.Collections.Generic;
using UnityEngine;

public class BulletObservable : MonoBehaviour, IObservable
{
    List<IObserver> _allObserver = new List<IObserver>();

    void Start()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Bullet_Hit, Bullet_Hit);
    }

    void Bullet_Hit(params object[] parameterContainer)
    {
        Debug.Log("Bullet_HIT_Event");
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
        NotifyToObservers("Event_Bullet_Hit");
    }
}
