using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Pool<BulletDefault> m_DefaultBullet;

    private void Start()
    {
        GameManager.Instance.PoolManager = this;
        m_DefaultBullet.Initialize(new GameObject("Default Bullet Pool").transform);
    }
}
