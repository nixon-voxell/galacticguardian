using System;
using System.Collections;
using System.Collections.Generic;
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

    public void AttackEnemy(Tower tower, Transform victim)
    {
        m_Tower = tower;
        m_Target = victim;
    }

    private void ShootEnemy()
    {
        if (m_Target != null && Time.time > m_NextAtkTime)
        {
            BulletDefault bullet = GameManager.Instance.PoolManager.m_EnemyBullet.GetNextObject();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Util.LookAt2DRotation(transform.position, m_Target.transform.position);
            bullet.StartBullet(m_Tower.BulletStat);

            m_NextAtkTime = Time.time + (1 / m_Tower.TowerAtkRate);
        }
    }

}
