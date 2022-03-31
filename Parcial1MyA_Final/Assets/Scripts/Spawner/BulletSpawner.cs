using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    static BulletSpawner _instance;
    public Bullet bulletPrefab;
    public int bulletStock = 7;

    public ObjectPool<Bullet> pool;
    public static BulletSpawner Instance
    {
        get
        {

            return _instance;
        }
    }

    void Start()
    {
        _instance = this;

        pool = new ObjectPool<Bullet>(BulletFactory, Bullet.TurnOn, Bullet.TurnOff, bulletStock, true);
    }

    public Bullet BulletFactory()
    {
        return Instantiate(bulletPrefab);
    }

    public void ReturnBullet(Bullet b)
    {
        pool.ReturnObject(b);
    }
}
