using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalBulletMove : IBulletMove
{
    private Transform transform;
    private float speed;
    private Vector3 pos;

    public float frequency = 20.0f;
    public float magnitude = 0.5f;
    private Vector3 axis;

    public SinusoidalBulletMove(float speed)
    {
        this.speed = speed;
        pos = transform.position;
        axis = transform.up;
    }
    public SinusoidalBulletMove(Transform transform, float speed)
    {
        this.transform = transform;
        this.speed = speed;
        pos = transform.position;
        axis = transform.up;
    }

    public void Move()
    {
        //float newPosX = transform.position.x + speed * Time.deltaTime;
        //float newPosY = Mathf.Sin(newPosX * 3) * 2;
        //transform.position = new Vector3(newPosX, newPosY, transform.position.z);

        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;

        //this.transform.position = transform.position + new Vector3(0, Mathf.Sin(Time.time * speed), speed * Time.deltaTime);
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
