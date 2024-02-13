using System.Collections;
using UnityEngine;

public class EnemySpawnerLevel : MonoBehaviour
{
    public GameObject[] m_EnemyPool;
    [SerializeField] private float m_SpawnRange;
    [SerializeField] private int m_SpawnAmt;
    [SerializeField] private float m_SpawnInterval;

    private bool m_Spawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.CompareTag("Tile")) && m_Spawned == false)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        m_Spawned = true;
        for (int i = 0; i < m_SpawnAmt; i++)
        {
            print("Enter");



            Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle.normalized * m_SpawnRange;
            int enemyIdx = Random.Range(0, m_EnemyPool.Length);
            GameObject enemyObj = Instantiate(m_EnemyPool[enemyIdx], randomPos, Quaternion.identity);
            enemyObj.GetComponent<Enemy>().InitializeEnemy();
            AudioManager.Instance.PlaySfx("EnemySpawn");

            yield return new WaitForSeconds(m_SpawnInterval);

        }


        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, m_SpawnRange);
    }
}
