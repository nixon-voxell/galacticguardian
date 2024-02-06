using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Pool<BulletDefault> m_EnemyBullet;
    public Pool<BulletDefault> m_TowerBullet;
    public Pool<BulletAtomic> m_AtomicBullet;

    private void Start()
    {
        LevelManager.Instance.PoolManager = this;
        m_EnemyBullet.Initialize(new GameObject("Default Bullet Pool").transform);
        m_TowerBullet.Initialize(new GameObject("Tower Bullet Pool").transform);
        m_AtomicBullet.Initialize(new GameObject("Atomic Bullet Pool").transform);
    }
}
