using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> enemies;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float timeSinceSpawned;
    private float spawnRate = 5;

    public int score;

    private bool gameEnded;

    public float mana; //temp name didnt know how to call this xD

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(gameEnded) return;

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
        scoreText.text = "Score: " + score.ToString("D5");
    }

    void SpawnNew()
    {
        
        int rand_Y = Random.Range(-5, 5);
        int rand_X = Random.Range(-7, 7);
        float r = Random.Range(0.0f, 1.0f);
        if(r > 0.5)
        {
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/SphereEnemy"), new Vector3(rand_X, rand_Y, 0), Quaternion.identity, enemyContainer.transform);
            enemies.Add(enemy);
        }
        else
        {
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"), new Vector3(rand_X, rand_Y, 0), Quaternion.identity, enemyContainer.transform);
            enemies.Add(enemy);
        }
        
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void EndGame()
    {
        gameOverScreen.SetActive(true);
        gameEnded = true;
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
}
