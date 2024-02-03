using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float TowerMaxHP;
    public float TowerDamage;
    public float TowerAtkRate;
    public float TowerAtkSpeed;
    public float TowerAtkRange;
    public LayerMask TowerAtkLayers;

    // Assign at runtime
    private Transform m_EnemyTarget;
    private BulletStat m_BulletStat;
    private ITower m_TowerBehaviour;
    private TileHealth m_TileHealth;

    public BulletStat BulletStat { get => m_BulletStat; }

    private void Awake()
    {
        m_TowerBehaviour = GetComponent<ITower>();
        m_TileHealth = GetComponent<TileHealth>();
    }

    private void Start()
    {
        InitializeTower();
    }

    private void Update()
    {
        // Check and get target
        EnemyDetection();
        
    }
    public void InitializeTower()
    {
        m_BulletStat = new BulletStat(TowerDamage, TowerAtkSpeed);
        m_TowerBehaviour?.InitializeBehaviour(this);
        m_TileHealth.InitializeTile(TowerMaxHP);
        // Set node health
    }


    private void EnemyDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, TowerAtkRange, TowerAtkLayers);
        if (colliders.Length > 0)
        {
            // Check if existing target is still within range
            if (Array.Exists<Collider2D>(colliders, collider => collider.transform == m_EnemyTarget))
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

            // Set target
            m_TowerBehaviour?.SetTargetLost(this, m_EnemyTarget);
            m_EnemyTarget = colliders[targetIdx].transform;
            m_TowerBehaviour?.SetNewTarget(this, m_EnemyTarget);
        }
        else
        {
            m_TowerBehaviour?.SetTargetLost(this, m_EnemyTarget);
            m_EnemyTarget = null;
        }
    }

}
