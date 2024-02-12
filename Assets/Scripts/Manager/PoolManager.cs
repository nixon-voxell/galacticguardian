using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Pool<BulletDefault> EnemyBullet;
    public Pool<BulletDefault> TowerBullet;
    public Pool<BulletAtomic> AtomicBullet;
    public Pool<Essence> Essence;


    // Effects
    public Pool<ParticleSystem> FxTileDestroyed;
    public Pool<ParticleSystem> FxDemonBulletHit;
    public Pool<ParticleSystem> FxEnemyDestroyed;
    public Pool<ParticleSystem> FxBloodSplash;

    private void Start()
    {
        LevelManager.Instance.PoolManager = this;
        EnemyBullet.Initialize(CreateParent("Enemy Bullet Pool"));
        TowerBullet.Initialize(CreateParent("Tower Bullet Pool"));
        AtomicBullet.Initialize(CreateParent("Atomic Bullet Pool"));
        Essence.Initialize(CreateParent("Essence Bullet Pool"));
        FxTileDestroyed.Initialize(CreateParent("Fx Tile Destroyed Pool"));
        FxDemonBulletHit.Initialize(CreateParent("Fx Tile Hit Pool"));
        FxEnemyDestroyed.Initialize(CreateParent("Fx Enemy Destroyed Pool"));
        FxBloodSplash.Initialize(CreateParent("Fx Blood Spash Pool"));
    }

    public T PlacePoolItemAt<T>(Vector2 position, Pool<T> pfxPool) where T : Component
    {
        T component = pfxPool.GetNextObject();
        Transform componentTrans = component.transform;
        componentTrans.position = position;
        componentTrans.gameObject.SetActive(false);
        componentTrans.gameObject.SetActive(true);

        return component;
    }

    private Transform CreateParent(string newChildName)
    {
        GameObject child = new GameObject(newChildName);
        child.transform.parent = this.transform;
        return child.transform;
    }
}
