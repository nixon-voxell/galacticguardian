using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : State
{
    private Enemy m_Enemy;
    private float m_NextAtkTime;
    private Transform m_Victim;
    private IDamageable m_Damageable;

    protected override void OnEnter()
    {
        m_Enemy = this.StateController as Enemy;
        m_Victim = m_Enemy.AtkTarget;
        m_Damageable = m_Victim.GetComponent<IDamageable>();
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
            m_Damageable.OnDamage(m_Enemy.transform, m_Enemy.EnemyDamage);
            m_NextAtkTime = Time.time + (1 / m_Enemy.EnemyAtkRate);
        }
    }
}
