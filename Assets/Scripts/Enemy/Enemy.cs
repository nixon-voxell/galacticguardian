using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateController, IDamageable
{
    public float EnemyMaxHP;
    public float EnemyDamage;
    public float EnemyMovementSpeed;
    public float EnemyAtkRate; 
    public float EnemyAtkSpeed; 
    public float EnemyAtkRange;
    public CircleCollider2D AtkCollider;

    [Header("Behaviours")] 
    public State AtkState;
    public EnemyChase ChaseState;

    [Header("Scaling")]
    [SerializeField] private float m_EnemyHPScale;
    [SerializeField] private float m_EnemyDamageScale;
    [SerializeField] private float m_EnemyMovementSpeedScale;
    [SerializeField] private float m_EnemyAtkSpeedScale;
    [SerializeField] private float m_EnemyAtkRateScale;

    // Assign at RunTime
    private float m_EnemyCurrentHP;
    private Transform m_AtkTarget;

    public Transform AtkTarget { get => m_AtkTarget;}

    // States


    private void Start()
    { 
        InitializeEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            m_AtkTarget = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            m_AtkTarget = null;
        }
    }

    private void Update()
    {
        
        EvaluateState();

        this.StateUpdate();
    }

    public void InitializeEnemy()
    {
        float time = GameStat.Instance.Time;

        // Enemy Scaling
        EnemyMaxHP += m_EnemyAtkRateScale * time;
        EnemyDamage += m_EnemyDamageScale * time;
        EnemyMovementSpeed += m_EnemyMovementSpeedScale * time;
        EnemyAtkRate += m_EnemyAtkRateScale * time;
        EnemyAtkSpeed += m_EnemyAtkSpeedScale * time;

        // Reassignation
        m_EnemyCurrentHP = EnemyMaxHP;
        AtkCollider.radius = EnemyAtkRange;
    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_EnemyCurrentHP -= damage;

        if (m_EnemyCurrentHP <= 0)
        {
            m_EnemyCurrentHP = 0;
            DestroyEnemy();
        }
    }

    private void EvaluateState()
    {
        // Change State To
        if (AtkTarget != null)
        {
            this.ChangeState(AtkState);
        }
        else
        {
            // Chasing
            this.ChangeState(ChaseState);
        }
    }

    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
        Debug.Log($"[SYSTEM] Enemy {transform.name} dead");
    }

}
