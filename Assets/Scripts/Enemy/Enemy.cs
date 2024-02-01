using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Chasing, Attacking };

    public float EnemyMaxHP;
    public float EnemyDamage;
    public float EnemyMovementSpeed;
    public float EnemyAtkRate;
    public float EnemyAtkRange;
    public CircleCollider2D AtkCollider;


    private float m_EnemyCurrentHP;
    private Transform m_AtkTarget;
    private EnemyState m_CurrentState;

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
    }


    private void EvaluateState()
    {
        if (m_AtkTarget != null)
        {
            m_CurrentState = EnemyState.Attacking;
            Attack();
        }
        else
        {
            m_CurrentState = EnemyState.Chasing;
            Chasing();
        }
    }
    private void InitializeEnemy()
    {
        m_EnemyCurrentHP = EnemyMaxHP;
        AtkCollider.radius = EnemyAtkRange;
    }

    private void Chasing()
    {
        Debug.Log("Chasing");
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }

    


}
