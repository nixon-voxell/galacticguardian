using UnityEngine;

public class LevelManager : SingletonMono<LevelManager>
{
    [HideInInspector] public Player Player;
    [HideInInspector] public PoolManager PoolManager;
}
