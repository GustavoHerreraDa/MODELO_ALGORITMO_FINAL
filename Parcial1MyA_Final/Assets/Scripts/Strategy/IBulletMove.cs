using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletMove
{
    void Move();

    void SetTransform(Transform transform);
    void SetSpeed(float speed);

}
