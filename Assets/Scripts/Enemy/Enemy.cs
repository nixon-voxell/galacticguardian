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
    public float EnemyAtkRange; // This stat not scaled
    public LayerMask AtkLayerMask;

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

    private void Update()
    {
        EnemyDetection();
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
    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_EnemyCurrentHP -= damage;

        Debug.Log("Take Damage: " + damage);


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
    private void EnemyDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyAtkRange, AtkLayerMask);
        if (colliders.Length > 0)
        {
            // Check if existing target is still within range
            if (Array.Exists<Collider2D>(colliders, collider => collider.transform == m_AtkTarget))
                return;

            // Get the closest target
            float nearestDist = float.PositiveInfinity;
            int targetIdx = 0;
            for (int i = 0; i < colliders.Length; i++)
            {
                float dist = Vector2.Distance(transform.position, colliders[i].transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    targetIdx = i;
                }
            }
            m_AtkTarget = colliders[targetIdx].transform;
        }
        else
        {
            m_AtkTarget = null;
        }
    }

    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
        Debug.Log($"[SYSTEM] Enemy {transform.name} dead");
    }

}
