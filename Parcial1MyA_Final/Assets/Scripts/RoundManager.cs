using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour, IObserver
{
    public Enemy enemyPrefab;

    public Transform spawnPoints;

    Transform[] _spawnPositions;

    Transform _target;

    int _totalEnemies;

    int _actualRound;

    LooKUpTable<int, int> totalEnemiesForactualRound;

    void Start()
    {
        initializeLookAtTable();

        _target = FindObjectOfType<PlayerModel>().transform; //target que le voy a asignar al enemigo

        _spawnPositions = spawnPoints.GetComponentsInChildren<Transform>(); //Los puntos de spawn para los enemigos

        StartCoroutine(SpawnEnemies());
    }

    public void EnemyDead()
    {
        _totalEnemies--; //Murio un enemy

        if (_totalEnemies <= 0) //Si no hay mas enemies
        {
            StartCoroutine(SpawnEnemies()); //Nueva wave
        }
    }

    //Devuelve cuantos enemigos crear por ronda
    int CalculateEnemiesToSpawn(int round)
    {
        return round * 2;
    }

    IEnumerator SpawnEnemies()
    {
        _actualRound++; //Nueva ronda

        _totalEnemies = totalEnemiesForactualRound.ReturnValue(_actualRound); //Total de enemigos a spawnear

        int enemiesToSpawn = _totalEnemies;

        int enemiesCont = 0;

        while (enemiesCont < enemiesToSpawn)
        {
            int posToSpawn = Random.Range(0, _spawnPositions.Length); //Posicion en la que va a spawnear

            /*var e = Instantiate(enemyPrefab, _spawnPositions[posToSpawn].position, Quaternion.identity);*/  //Instancio enemy
            var enemy = EnemySpawner.Instance.pool.GetObject();
            
            enemy.gameObject.GetComponent<EnemyObservable>().Subscribe(this);
            enemy.transform.position = _spawnPositions[posToSpawn].position;
            enemy.SetManager (this).SetTarget(_target); //Le paso el manager para que al morir le avise que reduza uno en _totalEnemies
            
            //e.target = _target; //Le paso el target

            enemiesCont++;

            yield return new WaitForSeconds(0.5f);
        }

    }

    public void Notify(string action)
    {
        if (action == "Event_Enemy_Destroy")
        {
            EnemyDead();
        }
    }

    void initializeLookAtTable()
    {
        totalEnemiesForactualRound = new LooKUpTable<int, int>(CalculateEnemiesToSpawn);

        for (var x = 1; x <= 10; x++)
        {
            totalEnemiesForactualRound.ReturnValue(x);
        }
    }
}
