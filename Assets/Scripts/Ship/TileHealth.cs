using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHealth : MonoBehaviour, IDamageable
{
    public float MaxHealth;
     
    private float m_CurrentHealth;

    private void OnEnable()
    {
        // Future purposes better to move it to a function to be called at the tile node handler
        m_CurrentHealth = MaxHealth;
    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_CurrentHealth -= damage;
        Debug.Log($"Node: {gameObject.name} | Health: {m_CurrentHealth} | Damage: {damage}");

        if (m_CurrentHealth <= 0 )
        {
            m_CurrentHealth = 0;
            Debug.Log("GAME OVER");
        }
    }
}