using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealBulletMove : IBulletMove
{
    private Transform transform;
    private float speed;

    public LinealBulletMove(Transform transform, float speed)
    {
        this.transform = transform;
        this.speed = speed;
    }

    public void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
