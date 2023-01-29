using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Enemy> enemies;

    [SerializeField] private List<Enemy> enemyPrefab;
    [SerializeField] private GameObject enemyContainer;

    private float timeSinceSpawned;
    private float spawnRate = 5;

    public int score;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(enemies.Count == 0)
        {
            SpawnNew();
        }

        if(timeSinceSpawned > spawnRate)
        {
            SpawnNew();
            timeSinceSpawned = 0;
        }
        timeSinceSpawned += Time.deltaTime;
    }

    void SpawnNew()
    {
        float rand_Y = Random.Range(-5,5);
        float rand_X = Random.Range(-7,7);
        Enemy enemy = Object.Instantiate(enemyPrefab[0], new Vector3(rand_X, rand_Y, 0),  Quaternion.identity, enemyContainer.transform);
        
        //enemy starts with shooting
        enemy.behaviour.ChangeState(EnemyState.Growing);

        this.enemies.Add(enemy);
    }

    public void SpawnFromKill(Vector3 deathLocation)
    {
        int spawnAmount = Random.Range(1,4);
        
        for(int i = 0; i < spawnAmount; i++)
        {
            //TODO choose a location within screen and near enemy
            float rand_Y = Random.Range(Mathf.Max(-5, deathLocation.y -3),Mathf.Min(5, deathLocation.y + 3));
            float rand_X = Random.Range(Mathf.Max(-7, deathLocation.x -3),Mathf.Min(7, deathLocation.x + 3));

            //shoot a seed away from deathLocation towards new location  
            Enemy enemy = Object.Instantiate(enemyPrefab[0], deathLocation,
              Quaternion.identity, enemyContainer.transform);
              enemy.behaviour.ChangeState(EnemyState.Seed);
              enemy.behaviour.target = new Vector3(rand_X, rand_Y, deathLocation.z);
            this.enemies.Add(enemy);
        }
    }
}
