using System;
using System.Collections;
using UnityEngine;

public class PlayerModel : MonoBehaviour, IObserver
{
    public float speed = 10;
    public float shootCooldown;
    public ObjectPool<Bullet> pool;
    public IBulletMove currentBulletMove;
    private BulletMove currentEnumBulletMove;

    private PlayerController controller;
    public Bullet bulletPrefab;

    public event Action isMoving = delegate { };
    public event Action shoot = delegate { };
    public event Action onDeath = delegate { };
    public event Action targetHit = delegate { };


    Coroutine _shootCDCor;
    public bool _canShoot;
    Camera _myCamera;
    public Transform pointToSpawn;



    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool<Bullet>(BulletFactory, Bullet.TurnOn, Bullet.TurnOff, 5, true);
        currentEnumBulletMove = BulletMove.lineal;
        _myCamera = Camera.main;
        controller = new PlayerController(this, GetComponentInChildren<PlayerView>());
        _canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        controller.OnUpdate();
    }

    public void ChangeLineal()
    {
        currentEnumBulletMove = BulletMove.lineal;
    }

    public void ChangeSinusoidal()
    {
        currentEnumBulletMove = BulletMove.Sinusoidal;
    }

    public void Shoot()
    {
        if (_canShoot)
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

            b.owner = this;
            _shootCDCor = StartCoroutine(ShootCooldown());

            shoot();
        }
    }
    public void Move(Vector3 direction)
    {
        transform.position += (_myCamera.transform.right * direction.x + _myCamera.transform.up * direction.y).normalized * speed * Time.deltaTime;

        if (direction.x != 0 || direction.y != 0)
            IsMoving();
    }

    public void IsMoving()
    {
        isMoving();
    }

    public void LookAt(Vector3 direction)
    {
        Vector3 lookAtPos = _myCamera.ScreenToWorldPoint(direction);
        lookAtPos.z = transform.position.z;
        transform.right = lookAtPos - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            onDeath();
            StartCoroutine(WaitOnSecond());
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

    public void TargetHit()
    {
        if (_shootCDCor != null)
        {
            StopCoroutine(_shootCDCor);
            targetHit();
        }

        _canShoot = true;

    }

    IEnumerator ShootCooldown()
    {
        _canShoot = false;

        float ticks = 0;

        while (ticks < shootCooldown)
        {
            ticks += Time.deltaTime;
            yield return null;
        }

        _canShoot = true;
    }

    IEnumerator WaitOnSecond()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
