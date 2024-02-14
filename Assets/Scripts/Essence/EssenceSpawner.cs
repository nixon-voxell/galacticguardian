using System.Collections;
using UnityEngine;

public class EssenceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_EssenceObj;
    [SerializeField] private int m_EssenceAmount;
    [SerializeField] private int m_EssenceSpawnCount;

    private const float SPAWN_RANGE = 1.5f;
    private const float DELAY_SPAWN = 1.5f;

    private Transform m_EssenceHolder;
    private bool m_EssenceSpawned = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        m_EssenceHolder = new GameObject("Essence Holder").transform;
        m_EssenceHolder.parent = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Tile"))
        {
            if (!m_EssenceSpawned)
            {
                StartSpawnEssence();
                m_EssenceSpawned = true;
            }
            else
            {
                CullEssence(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Tile"))
        {
            CullEssence(false);
        }
    }

    private void CullEssence(bool setActive)
    {
        m_EssenceHolder.gameObject.SetActive(setActive);
    }

    private void StartSpawnEssence()
    {
        for (int i = 0; i < m_EssenceSpawnCount; i++)
        {
            Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * SPAWN_RANGE;
            GameObject essenceObj = Instantiate(m_EssenceObj, randomPos, Quaternion.identity, m_EssenceHolder);
            essenceObj.GetComponent<Essence>().InitializeObject(m_EssenceAmount);
        }
    }
}
