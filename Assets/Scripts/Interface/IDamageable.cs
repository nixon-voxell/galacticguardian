using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void OnDamage(Transform attacker, float damage);
}
