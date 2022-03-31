using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static EnemySpawner _instance;
    public Enemy bulletPrefab;
    public int bulletStock = 7;

    public ObjectPool<Enemy> pool;
    public static EnemySpawner Instance
    {
        get
        {

            return _instance;
        }
    }

    void Start()
    {
        _instance = this;

        pool = new ObjectPool<Enemy>(EnemyFactory, Enemy.TurnOn, Enemy.TurnOff, bulletStock, true);
    }

    public Enemy EnemyFactory()
    {
        return Instantiate(bulletPrefab);
    }

    public void ReturnBullet(Enemy e)
    {
        pool.ReturnObject(e);
    }
}
