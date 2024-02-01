using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float MaxHealth;
     
    private float m_CurrentHealth;

    private void Start()
    {
        m_CurrentHealth = MaxHealth;
    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_CurrentHealth -= damage;
        Debug.Log($"Health: {m_CurrentHealth} | Damage: {damage}");

        if (m_CurrentHealth <= 0 )
        {
            m_CurrentHealth = 0;
            Debug.Log("GAME OVER");
        }
    }
}
