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
        if (m_Target != null && Time.time > m_NextAtkTime)
        {
            BulletDefault bullet = LevelManager.Instance.PoolManager.TowerBullet.GetNextObject();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Util.LookAt2DRotation(transform.position, m_Target.transform.position);
            bullet.StartBullet(m_Tower.BulletStat);

            m_NextAtkTime = Time.time + (1 / m_Tower.TowerAtkRate);
        }
    }

    public void SetNewTarget(Tower tower, Transform victim)
    {
        m_Target = victim;
    }

    public void SetTargetLost(Tower tower, Transform victim)
    {
        m_Target = null;
    }

    public void InitializeBehaviour(Tower tower)
    {
        m_Tower = tower;
    }
}
