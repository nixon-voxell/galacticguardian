using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Pool<BulletDefault> EnemyBullet;
    public Pool<BulletDefault> TowerBullet;
    public Pool<BulletAtomic> AtomicBullet;
    public Pool<Essence> Essence;

    private GameObject m_PoolParent;

    private void Start()
    {
        LevelManager.Instance.PoolManager = this;
        m_PoolParent = new GameObject("Pool Manager Parent");
        EnemyBullet.Initialize(CreateParent("Enemy Bullet Pool"));
        TowerBullet.Initialize(CreateParent("Tower Bullet Pool"));
        AtomicBullet.Initialize(CreateParent("Atomic Bullet Pool"));
        Essence.Initialize(CreateParent("Essence Bullet Pool"));
    }

    private Transform CreateParent(string newChildName)
    {
        GameObject child = new GameObject(newChildName);
        child.transform.parent = m_PoolParent.transform;
        return child.transform;
    }
}
