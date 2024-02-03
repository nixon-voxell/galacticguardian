using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : State
{
    [SerializeField] private float m_MeleeAtkRange;
    [SerializeField] private Enemy m_Enemy;

    private float m_NextAtkTime;
    private Transform m_Victim;
    private float m_Speed;
    private Rigidbody2D m_EnemyRb;

    private void Awake()
    {
        m_EnemyRb = m_Enemy.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AttackEnemyInRange();
    }

    private void AttackEnemyInRange()
    {
        if (m_Victim == null) return;

        Collider2D collider = Physics2D.OverlapCircle(transform.position, m_MeleeAtkRange, m_Enemy.AtkLayerMask);
        if (collider != null && Time.time > m_NextAtkTime)
        {
            collider.transform.GetComponent<IDamageable>().OnDamage(m_Enemy.transform, m_Enemy.EnemyDamage);
            m_NextAtkTime = Time.time + (1 / m_Enemy.EnemyAtkRate);
        }

    }

    private void FixedUpdate()
    {
        if (m_Victim != null)
        {
            Vector3 dir = m_Victim.transform.position - m_Enemy.transform.position;
            m_EnemyRb.velocity = dir.normalized * m_Speed;
        }
    }

    protected override void OnEnter()
    {
        m_Victim = m_Enemy.AtkTarget;
        m_Speed = m_Enemy.EnemyMovementSpeed;
    }

    protected override void OnExit()
    {
        m_Victim = null;
    }

    protected override void OnUpdate()
    {
        if (m_Victim == null)
            Debug.LogWarning("[SYSTEM] Melee Atk: Victim is null");

        
    }
}
