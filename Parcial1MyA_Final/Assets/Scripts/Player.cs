using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IObserver
{
    public float speed;
    public float shootCooldown;
    public Bullet bulletPrefab;
    public Transform pointToSpawn;
    public Image cooldownBar;
    public ObjectPool<Bullet> pool;

    public IBulletMove currentBulletMove;
    private BulletMove currentEnumBulletMove;

    Camera _myCamera;
    bool _canShoot;
    Coroutine _shootCDCor;


    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool<Bullet>(BulletFactory, Bullet.TurnOn, Bullet.TurnOff);
        currentEnumBulletMove = BulletMove.lineal;

        _myCamera = Camera.main;
        _canShoot = true;
        CompletedFireCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        Vector3 lookAtPos = _myCamera.ScreenToWorldPoint(Input.mousePosition);
        lookAtPos.z = transform.position.z;
        transform.right = lookAtPos - transform.position;

        transform.position += (_myCamera.transform.right * Input.GetAxisRaw("Horizontal") + _myCamera.transform.up * Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;

        //Disparo
        if (Input.GetMouseButtonDown(0))
        {
            if (_canShoot) Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentEnumBulletMove = BulletMove.lineal;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentEnumBulletMove = BulletMove.Sinusoidal;
        }
    }

    //void Shoot()
    //{
    //    Bullet b = Instantiate(bulletPrefab, pointToSpawn.position, transform.rotation); //Instancio bala
    //    b.timeToDie = shootCooldown;  //Le paso el cooldown como tiempo de vida
    //    b.owner = this;  //Le paso que el owner es este script para que cuando mate un enemigo me avise

    //    _shootCDCor = StartCoroutine(ShootCooldown());  //Corrutina del cooldown para volver a disparar
    //}

    void Shoot()
    {
        var b = pool.GetObject();
        b.gameObject.GetComponent<BulletObservable>().Subscribe(this);
        b.transform.position = pointToSpawn.position;
        b.transform.rotation = transform.rotation;
        b.timeToDie = shootCooldown;

        currentBulletMove = new LinealBulletMove(b.transform, b.speed);
        if (currentEnumBulletMove == BulletMove.Sinusoidal)
            currentBulletMove = new SinusoidalBulletMove(b.transform, b.speed);


        b.SetCurrentBulletMove(currentBulletMove);



        //b.owner = this;
        _shootCDCor = StartCoroutine(ShootCooldown());
    }

    //Funcion para cuando la bala toca un enemigo
    public void TargetHit()
    {
        if (_shootCDCor != null)
        {
            StopCoroutine(_shootCDCor);
        }

        _canShoot = true;
        CompletedFireCooldown();

    }

    //Setea cambios de la barra de CD del UI
    public void CompletedFireCooldown()
    {
        cooldownBar.color = Color.green;
        cooldownBar.fillAmount = 1;
    }

    IEnumerator ShootCooldown()
    {
        _canShoot = false;

        float ticks = 0;

        cooldownBar.color = Color.red;
        cooldownBar.fillAmount = 0;

        while (ticks < shootCooldown)
        {
            ticks += Time.deltaTime;
            cooldownBar.fillAmount = ticks;
            yield return null;
        }

        CompletedFireCooldown();
        _canShoot = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    public Bullet BulletFactory()
    {
        return Instantiate(bulletPrefab);
    }

    public void ReturnBullet(Bullet b)
    {
        pool.ReturnObject(b);
    }

    public void Notify(string action)
    {
        if (action == "Event_Bullet_Hit")
        {
            TargetHit();
        }
    }
}
