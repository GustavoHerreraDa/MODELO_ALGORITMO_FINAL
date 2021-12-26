using UnityEngine;

public enum BulletMove
{
    lineal,
    Sinusoidal
}
public class Bullet : MonoBehaviour
{
    public float speed;
    public float timeToDie;
    public PlayerModel owner;

    IBulletMove linealBulletMove;
    IBulletMove sinusoidalBulletMove;
    IBulletMove currentBullet;


    void Awake()
    {
        linealBulletMove = new LinealBulletMove(transform, this.speed);
        sinusoidalBulletMove = new SinusoidalBulletMove(transform, this.speed);

        currentBullet = linealBulletMove;
        owner = FindObjectOfType<PlayerModel>().GetComponent<PlayerModel>();
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
}
