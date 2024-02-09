using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Pool<Enemy>[] m_EnemyPool;

    [Header("Spawner Stats")]
    [SerializeField] private float m_SpawnOffRange;
    [SerializeField] private float m_InitialSpawnRate;
    [SerializeField] private float m_SpawnRateIncrease;
    [SerializeField] private float m_MaxSpawnRate;

    // Assigned on runtime
    [HideInInspector] public bool SpawnActive;
    private float m_CurrentSpawnRate;

    private void Start()
    {
        foreach (Pool<Enemy> pool in m_EnemyPool)
        {
            pool.Initialize(transform);
        }

        m_CurrentSpawnRate = m_InitialSpawnRate;

        Util.CallFunctionNextFrame(this, () =>
        {
            SpawnActive = true;
            StartCoroutine(StartSpawning());
        });

    }

    private void Update()
    {
        if (m_CurrentSpawnRate < m_MaxSpawnRate)
            m_CurrentSpawnRate += m_SpawnRateIncrease * Time.deltaTime;
    }

    private IEnumerator StartSpawning()
    {
        while (true)
        {
            // Pause spawning
            if (!SpawnActive)
                yield return null;

            SpawnEnemy();
            yield return new WaitForSeconds(1 / m_CurrentSpawnRate);
        }
    }

    private void SpawnEnemy()
    {
        int enemyIdx = Random.Range(0, m_EnemyPool.Length);
        Vector2 spawnPos = GetRandomPos();
        Enemy _enemy = m_EnemyPool[enemyIdx].GetNextObject();
        _enemy.transform.position = spawnPos;
        _enemy.gameObject.SetActive(true);
        _enemy.InitializeEnemy();
    }

    private Vector2 GetRandomPos()
    {
        Vector2 randomDirection = -Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)LevelManager.Instance.Player.transform.position + randomDirection * m_SpawnOffRange;

        return spawnPosition;
    }
}
