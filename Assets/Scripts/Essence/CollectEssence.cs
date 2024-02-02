using UnityEngine;

public class CollectEssence : GameStat
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Essence"))
        {
            EssenceCount++;
            Debug.Log("Essence Count: " + EssenceCount);
            Destroy(other.gameObject);
            
        }
    }
}
