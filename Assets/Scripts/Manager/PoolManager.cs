using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Pool<BulletDefault> EnemyBullet;
    public Pool<BulletDefault> TowerBullet;
    public Pool<BulletAtomic> AtomicBullet;
    public Pool<Essence> Essence;


    // Effects 
    public Pool<ParticleSystem> FxNodeDestroyed;

    private void Start()
    {
        LevelManager.Instance.PoolManager = this;
        EnemyBullet.Initialize(CreateParent("Enemy Bullet Pool"));
        TowerBullet.Initialize(CreateParent("Tower Bullet Pool"));
        AtomicBullet.Initialize(CreateParent("Atomic Bullet Pool"));
        Essence.Initialize(CreateParent("Essence Bullet Pool"));
        FxNodeDestroyed.Initialize(CreateParent("Fx Node Destroyed Pool"));
    }

    private Transform CreateParent(string newChildName)
    {
        GameObject child = new GameObject(newChildName);
        child.transform.parent = this.transform;
        return child.transform;
    }
}
