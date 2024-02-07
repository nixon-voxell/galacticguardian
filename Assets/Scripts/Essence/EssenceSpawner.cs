using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceSpawner : MonoBehaviour
{
    [SerializeField] private int m_EssenceAmount;

    private void Start()
    {
        Util.CallFunctionNextFrame(this, () =>
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Essence essence = LevelManager.Instance.PoolManager.Essence.GetNextObject();
            essence.InitializeObject(m_EssenceAmount);
            essence.transform.position = transform.position;
        });
        
    }
}
