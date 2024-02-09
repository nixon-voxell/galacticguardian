using UnityEngine;

public class TowerAtomicAtk : MonoBehaviour, ITower
{
    [SerializeField] private float m_AOERadius;
    [SerializeField] private float m_HitDelay;
    [SerializeField] private GameObject m_ShootPfx;

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
            BulletAtomic bullet = LevelManager.Instance.PoolManager.AtomicBullet.GetNextObject();
            bullet.transform.position = m_Target.position;
            bullet.StartBullet(m_AOERadius, m_Tower.TowerDamage, m_HitDelay, m_Tower.TowerAtkLayers);
            m_ShootPfx.SetActive(false);
            m_ShootPfx.SetActive(true);

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
