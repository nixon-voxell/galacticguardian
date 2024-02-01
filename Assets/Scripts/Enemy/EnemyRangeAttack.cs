using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : State
{
    [SerializeField] private float m_BulletSpeed;

    private Enemy m_Enemy;
    private float m_NextAtkTime;
    private Transform m_Victim;

    protected override void OnEnter()
    {
        m_Enemy = this.StateController as Enemy;
        m_Victim = m_Enemy.AtkTarget;
    }

    protected override void OnExit()
    {
        m_Victim = null;
    }

    protected override void OnUpdate()
    {
        if (m_Victim == null)
            Debug.LogWarning("[SYSTEM] Melee Atk: Victim is null");

        if (Time.time > m_NextAtkTime)
        {
            Debug.Log("Enemy range spawn bullet");
            BulletDefault bullet =  GameManager.Instance.PoolManager.m_DefaultBullet.GetNextObject();
            bullet.transform.position = m_Enemy.transform.position;
            bullet.transform.rotation = Util.LookAt2DRotation(m_Enemy.transform.position, m_Victim.transform.position);
            bullet.StartBullet(new BulletStat(m_Enemy.EnemyDamage, m_BulletSpeed));
            m_NextAtkTime = Time.time + (1 / m_Enemy.EnemyAtkRate);
        }
    }
}
