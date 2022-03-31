using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObservable
{
    private Transform target;

    private RoundManager manager;

    List<IObserver> _allObserver = new List<IObserver>();

    public Enemy SetManager(RoundManager manager)
    {
        this.manager = manager;
        return this;
    }

    public Enemy SetTarget(Transform transform)
    {
        this.target = transform;
        return this;
    }

    void Start()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Enemy_Destroy, Event_Enemy_Destroy);
    }


    // Update is called once per frame
    void Update()
    {
        if (!target) return; //Si no hay target no hago nada

        //Movimiento
        Vector3 dir = target.position - transform.position;
        dir.z = target.position.z;
        dir.Normalize();
        transform.position += dir * FlyWeightPointer.Asteriod.enemy_speed * Time.deltaTime;
    }

    public void GetShot()
    {
        //manager.EnemyDead(); //Le digo al manager que mori
        //Destroy(gameObject); //Me destruyo
        NotifyToObservers("Event_Enemy_Destroy");
        TurnOff(this);
    }

    private void Reset()
    {
    }

    public static void TurnOn(Enemy b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    void Event_Enemy_Destroy(params object[] parameterContainer)
    {
        Debug.Log("Event_Enemy_Destroy");
    }

    public static void TurnOff(Enemy b)
    {
        b.gameObject.SetActive(false);
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
}
