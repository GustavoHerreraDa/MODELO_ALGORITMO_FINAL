using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IObservable
{
    public float speed;
    public float timeToDie;
    public PlayerModel owner;

    IBulletMove linealBulletMove;
    IBulletMove sinusoidalBulletMove;
    IBulletMove currentBullet;

    List<IObserver> _allObserver = new List<IObserver>();


    void Awake()
    {
        linealBulletMove = new LinealBulletMove(transform, this.speed);
        sinusoidalBulletMove = new SinusoidalBulletMove(transform, this.speed);

        currentBullet = linealBulletMove;
        owner = FindObjectOfType<PlayerModel>().GetComponent<PlayerModel>();

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Bullet_Hit, Bullet_Hit);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        currentBullet.Move();

        //Lifetime
        timeToDie -= Time.deltaTime;

        if (timeToDie <= 0)
        {
            TurnOff(this);
        }
    }

    public Bullet SetCurrentBulletMove(IBulletMove bulletMove)
    {
        this.currentBullet = bulletMove;
        
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy en = collision.GetComponent<Enemy>();

        if (en)
        {
            //owner.TargetHit(); //Le digo al player que le pegue

            en.GetShot(); //Le hago damage al enemigo

            NotifyToObservers("Event_Bullet_Hit");

            /*Destroy(gameObject)*/
            //Me destruyo
            TurnOff(this);
        }
    }

    private void Reset()
    {
    }

    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }
    void Bullet_Hit(params object[] parameterContainer)
    {
        Debug.Log("Bullet_HIT_Event");
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

    public void NotifyToObservers(string action)
    {
        for (int i = _allObserver.Count - 1; i >= 0; i--)
        {
            _allObserver[i].Notify(action);
        }
    }


}
