using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public float spawnInterval = 2f;
    public float difficultyIncreaseInterval = 10f;
    public float difficultyIncreaseAmount = 0.1f;
    public float spawnDistance = 20f;
    public float maxEnemySpeed = 20f;
    public float minSpawnDistance = 10f;
    public float maxSpawnOffset = 5f;

    private float nextSpawnTime;
    private float nextDifficultyIncreaseTime;
    private int waveCount = 0;
    private float currentEnemySpeed;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        nextDifficultyIncreaseTime = Time.time + difficultyIncreaseInterval;
        currentEnemySpeed = enemyPrefab1.GetComponent<Enemy1>().enemy1Speed;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }

        if (Time.time >= nextDifficultyIncreaseTime)
        {
            IncreaseDifficulty();
            nextDifficultyIncreaseTime = Time.time + difficultyIncreaseInterval;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        GameObject enemyPrefab = Random.Range(0, 2) == 0 ? enemyPrefab1 : enemyPrefab2;

        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy1>().enemy1Speed = currentEnemySpeed;
    }

    Vector3 CalculateSpawnPosition()
    {
        if (GameObject.FindGameObjectWithTag("Player") is GameObject player)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            randomDirection.y = 0;

            Vector3 spawnPosition = playerPosition + randomDirection * spawnDistance;

            if (Vector3.Distance(spawnPosition, playerPosition) < minSpawnDistance)
            {
                spawnPosition = playerPosition + (spawnPosition - playerPosition).normalized * minSpawnDistance;
            }

            Vector3 offset = new Vector3(Random.Range(-maxSpawnOffset, maxSpawnOffset), 0, Random.Range(-maxSpawnOffset, maxSpawnOffset));
            spawnPosition += offset;

            return spawnPosition;
        }
        else
        {
            return Vector3.zero;
        }
    }

    void IncreaseDifficulty()
    {
        waveCount++;
        spawnInterval = Mathf.Max(spawnInterval - 0.5f, 0.1f);
        currentEnemySpeed = Mathf.Min(currentEnemySpeed + 1f, maxEnemySpeed);
    }
}
