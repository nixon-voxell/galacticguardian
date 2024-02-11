// using System;
using MilkShake;
using UnityEngine;

[RequireComponent(typeof(TileNode))]
public class TileHealth : MonoBehaviour, IDamageable
{
    public float MaxHealth;
    public float Health;
    private TileNode m_TileNode;

    public void OnDamage(Transform attacker, float damage)
    {
        this.Health -= damage;
        Debug.Log($"Node: {gameObject.name} | Health: {Health} | Damage: {damage}");

        if (this.Health <= 0)
        {
            this.m_TileNode.DestroyTile();

            // FX
            ShakerManager.Instance.Shake("TileDestroyed");
            GameObject pfx = LevelManager.Instance.PoolManager.FxNodeDestroyed.GetNextObject().gameObject;
            pfx.transform.position = transform.position;
            pfx.SetActive(true);
        }
    }

    public void Initialize(float currentHealth, float maxHealth)
    {
        this.Health = currentHealth;
        this.MaxHealth = maxHealth;
    }

    private void Awake()
    {
        this.m_TileNode = this.GetComponent<TileNode>();
    }
}
