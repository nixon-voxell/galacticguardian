using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public void SetNewTarget(Tower tower, Transform victim);
    public void SetTargetLost(Tower tower, Transform victim);
    public void InitializeBehaviour(Tower tower);
}
