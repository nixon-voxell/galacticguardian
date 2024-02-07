// using System;
using UnityEngine;

[RequireComponent(typeof(TileNode))]
public class TileHealth : MonoBehaviour, IDamageable
{
    private float m_CurrentHealth;
    private TileNode m_TileNode;

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
            this.m_TileNode.DestroyTile();
        }
    }

    private void Awake()
    {
        this.m_TileNode = this.GetComponent<TileNode>();
    }
}
