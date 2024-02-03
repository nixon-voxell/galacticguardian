using UnityEngine;

public class CollectEssence : GameStat
{
    [SerializeField] private GameObject m_Explosion;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Essence"))
        {
            EssenceCount++;
            Debug.Log("Essence Count: " + EssenceCount);
            if(m_Explosion != null)
            {
                Instantiate(m_Explosion, other.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
            
        }
    }
}
