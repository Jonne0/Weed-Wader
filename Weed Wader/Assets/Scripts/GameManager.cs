using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> enemies;

    [SerializeField] private GameObject enemyContainer;

    private float timeSinceSpawned;
    private float spawnRate = 5;

    public int score;

    public float mana; //temp name didnt know how to call this xD

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (enemies.Count == 0)
        {
            SpawnNew();
        }

        if (timeSinceSpawned > spawnRate)
        {
            SpawnNew();
            timeSinceSpawned = 0;
        }
        timeSinceSpawned += Time.deltaTime;
    }

    void SpawnNew()
    {
        int rand_Y = Random.Range(-5, 5);
        int rand_X = Random.Range(-7, 7);
        GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"), new Vector3(rand_X, rand_Y, 0), Quaternion.identity, enemyContainer.transform);
        enemies.Add(enemy);
    }
}
