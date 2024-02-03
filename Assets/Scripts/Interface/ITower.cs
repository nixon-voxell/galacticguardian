using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public void AttackEnemy(Tower tower, Transform victim);
}
