using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : StateController
{
    public float EnemyMaxHP;
    public float EnemyDamage;
    public float EnemyMovementSpeed;
    public float EnemyAtkRate; 
    public float EnemyAtkRange;
    public CircleCollider2D AtkCollider;

    [Header("Behaviours")] 
    public State AtkState;
    public EnemyChase ChaseState; 

    // Assign at RunTime
    private float m_EnemyCurrentHP;
    private Transform m_AtkTarget;

    public Transform AtkTarget { get => m_AtkTarget;}

    // States


    private void Awake()
    { 
        InitializeEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_AtkTarget = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_AtkTarget = null;
        }
    }

    private void Update()
    {
        
        EvaluateState();

        this.StateUpdate();
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
    private void InitializeEnemy()
    {
        m_EnemyCurrentHP = EnemyMaxHP;
        AtkCollider.radius = EnemyAtkRange;
    }
}
