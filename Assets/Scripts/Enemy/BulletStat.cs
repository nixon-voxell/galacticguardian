using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletStat
{
    public float BulletDamage;
    public float BulletSpeed;
    public float BulletLifetime;
    public int EnemyGoThroughCount;
    public bool PierceThroughEnemy;

    public BulletStat(float bulletDamage, float bulletSpeed, float bulletLifetime, int enemyGoThroughCount, bool pierceThroughEnemy, Transform bulletTarget)
    {
        BulletDamage = bulletDamage;
        BulletSpeed = bulletSpeed;
        BulletLifetime = bulletLifetime;
        EnemyGoThroughCount = enemyGoThroughCount;
        PierceThroughEnemy = pierceThroughEnemy;
    }


    public BulletStat(float bulletDamage, float bulletSpeed, float bulletLifetime = 3.0f)
    {
        BulletDamage = bulletDamage;
        BulletSpeed = bulletSpeed;
        BulletLifetime = bulletLifetime;
        EnemyGoThroughCount = 0;
        PierceThroughEnemy = false;
    }
}
