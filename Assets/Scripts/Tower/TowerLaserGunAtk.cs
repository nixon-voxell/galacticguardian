using UnityEngine;

public class TowerLaserGunAtk : MonoBehaviour, ITower
{
    private Tower m_Tower;
    private Transform m_Target;
    private float m_NextAtkTime;

    private void Update()
    {
        ShootEnemy();
    }

    private void ShootEnemy()
    {
        if (this.m_Target == null || Time.time < this.m_NextAtkTime)
        {
            return;
        }

        BulletDefault bullet = LevelManager.Instance.PoolManager.TowerBullet.GetNextObject();
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = Util.LookAt2DRotation(this.transform.position, this.m_Target.position);
        bullet.StartBullet(this.m_Tower.BulletStat);

        this.m_NextAtkTime = Time.time + (1 / this.m_Tower.TowerAtkRate);
    }

    public void SetNewTarget(Tower tower, Transform victim)
    {
        this.m_Target = victim;
    }

    public void SetTargetLost(Tower tower, Transform victim)
    {
        this.m_Target = null;
    }

    public void InitializeBehaviour(Tower tower)
    {
        this.m_Tower = tower;
    }
}
