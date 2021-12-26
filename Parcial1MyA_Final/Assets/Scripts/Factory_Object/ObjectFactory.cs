using UnityEngine;

public enum ObjectType
{
    TypeBullet,
    TypeEnemy
}

public class ObjectFactory
{
    ObjectType _type;

    private ObjectPool<IObject> pool;

    private GameObject prefab;

    public ObjectFactory()
    {
    }

    public IObject Get(ObjectType type)
    {
        _type = type;
        return GetObject();
    }

    public IObject GetObject()
    {
        IObject obj = null;

        switch (_type)
        {
            case ObjectType.TypeEnemy:
                return obj = pool.GetObject();
            case ObjectType.TypeBullet:
                return obj = pool.GetObject();
        }

        return obj;
    }
}


