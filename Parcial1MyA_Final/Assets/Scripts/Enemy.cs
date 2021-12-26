using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;

    private RoundManager manager;

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

    public static void TurnOff(Enemy b)
    {
        b.gameObject.SetActive(false);
    }
}
