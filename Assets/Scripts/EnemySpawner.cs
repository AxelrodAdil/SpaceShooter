using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;

    float maxSpawnRateInSeconds = 5f;
    GameObject scoreUIText;
    
    public void Start()
    {
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds = maxSpawnRateInSeconds > 1f ? Random.Range(1f, maxSpawnRateInSeconds) : 1f;
        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (scoreUIText.GetComponent<GameScore>().Score / 1000 > 0)
        {
            maxSpawnRateInSeconds = 2f;
        }
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    public void ScheduleEnemySpawner()
    {
        maxSpawnRateInSeconds = scoreUIText.GetComponent<GameScore>().Score / 1000 > 0 ? 2f : 5f;
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
