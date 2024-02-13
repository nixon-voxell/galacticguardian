using System.Collections;
using UnityEngine;

public class EssenceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_EssenceObj;
    [SerializeField] private int m_EssenceAmount;
    [SerializeField] private int m_EssenceSpawnCount;

    private const float SPAWN_RANGE = 1.5f;
    private const float DELAY_SPAWN = 1.5f;


    private void Start()
    {
        StartCoroutine(StartSpawnEssence());
    }

    private IEnumerator StartSpawnEssence()
    {
        // Delay spawn so won't lag the game on start
        yield return new WaitForSeconds(DELAY_SPAWN);

        GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < m_EssenceSpawnCount; i++)
        {
            Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle.normalized * SPAWN_RANGE;
            GameObject essenceObj = Instantiate(m_EssenceObj, randomPos, Quaternion.identity, this.transform);
            essenceObj.transform.parent = null;
            essenceObj.GetComponent<Essence>().InitializeObject(m_EssenceAmount);
            yield return null;
        }

        Destroy(gameObject);
    }
}
