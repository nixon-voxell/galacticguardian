
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour, IDamageable
{
    public void OnDamage(Transform attacker, float damage)
    {
        Debug.Log($"[TEST] Victim: {gameObject.name} | Damage: {damage}");
    }
}
