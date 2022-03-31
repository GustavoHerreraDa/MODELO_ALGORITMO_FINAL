using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealBulletMove : IBulletMove
{
    private Transform transform;
    private float speed;

    public LinealBulletMove(float speed)
    {
        this.speed = speed;

    }
    public LinealBulletMove(Transform transform, float speed)
    {
        this.transform = transform;
        this.speed = speed;
    }

    public void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
    }
}
