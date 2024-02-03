using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealth : MonoBehaviour, IDamageable
{
    public float MaxHealth;
     
    private float m_CurrentHealth;

    public event Action<Transform, float> OnDamageEvent; // Transform - Attacker | float - Damage
    public event Action<Transform> OnKilledEvent; // Transform - Killer


    private void OnEnable()
    {
        InitializeTile();
    }

    public void InitializeTile(float newHP = -1)
    {
        if (newHP == -1)
            m_CurrentHealth = MaxHealth;
        else
            m_CurrentHealth = newHP;

    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_CurrentHealth -= damage;
        Debug.Log($"Node: {gameObject.name} | Health: {m_CurrentHealth} | Damage: {damage}");

        if (m_CurrentHealth <= 0 )
        {
            m_CurrentHealth = 0;

            // For now to debug. I destroy itself
            // Do the destroy disabling stuff here
            Destroy(gameObject);
        }
    }
}
