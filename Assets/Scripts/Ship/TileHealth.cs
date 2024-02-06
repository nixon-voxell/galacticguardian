// using System;
using UnityEngine;

public class TileHealth : MonoBehaviour, IDamageable
{
    private float m_CurrentHealth;

    // public event Action<Transform, float> OnDamageEvent; // Transform - Attacker | float - Damage
    // public event Action<Transform> OnKilledEvent; // Transform - Killer

    public void InitializeTile(float health)
    {
        m_CurrentHealth = health;
    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_CurrentHealth -= damage;
        Debug.Log($"Node: {gameObject.name} | Health: {m_CurrentHealth} | Damage: {damage}");

        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;

            // For now to debug. I destroy itself
            // Do the destroy disabling stuff here
            Destroy(gameObject);
        }
    }
}
