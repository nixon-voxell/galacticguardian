using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct EnemyStat
{
    public float Health;
    public float Damage;
    public float Speed;
    public int EssenceDrop;

    public float AtkRate;
    public float AtkSpeed;

    public static EnemyStat Lerp(EnemyStat startStat, EnemyStat endStat, float time)
    {
        return new EnemyStat
        {
            Health = Mathf.Lerp(startStat.Health, endStat.Health, time),
            Damage = Mathf.Lerp(startStat.Damage, endStat.Damage, time),
            Speed = Mathf.Lerp(startStat.Speed, endStat.Speed, time),
            EssenceDrop = (int)Mathf.Lerp(startStat.EssenceDrop, endStat.EssenceDrop, time),
            AtkRate = Mathf.Lerp(startStat.AtkRate, endStat.AtkRate, time),
            AtkSpeed = Mathf.Lerp(startStat.AtkSpeed, endStat.AtkSpeed, time),
        };
    }
}

public class Enemy : StateController, IDamageable
{
    [SerializeField] private EnemyStat m_BaseStat;
    [SerializeField] private EnemyStat m_MaxStat;

    public float ProgressionDuration;

    public float EnemyMaxHP;
    public float EnemyDamage;
    public float EnemyMovementSpeed;
    public float EnemyAtkRate;
    public float EnemyAtkSpeed;
    public float EnemyAtkRange; // This stat not scaled
    public int EssenceDropAmt; // This stat not scaled
    public LayerMask AtkLayerMask;

    [Header("Behaviours")]
    public State AtkState;
    public EnemyChase ChaseState;

    [Header("Components")]
    [SerializeField] private GameObject m_DamagedModel;

    private const float DAMAGE_DURATION = 0.25f;


    // Assign at RunTime
    private float m_EnemyCurrentHP;
    private Transform m_AtkTarget;
    private bool m_Damaged = false;

    public Transform AtkTarget { get => m_AtkTarget; }

    private void Start()
    {
        InitializeEnemy();
    }

    private void Update()
    {
        EnemyDetection();
        EvaluateState();
        AdjustRotation();

        this.StateUpdate();
    }

    public void InitializeEnemy()
    {
        EnemyStat currStat = EnemyStat.Lerp(
            this.m_BaseStat,
            this.m_MaxStat,
            GameStat.Instance.Time / this.ProgressionDuration
        );

        // Enemy Scaling
        EnemyMaxHP = currStat.Health;
        EnemyDamage = currStat.Damage;
        EnemyMovementSpeed = currStat.Speed;
        EnemyAtkRate = currStat.AtkRate;
        EnemyAtkSpeed = currStat.AtkSpeed;
        EssenceDropAmt = currStat.EssenceDrop;

        // Reassignation
        m_EnemyCurrentHP = EnemyMaxHP;

        m_DamagedModel.SetActive(false);
    }

    public void OnDamage(Transform attacker, float damage)
    {
        m_EnemyCurrentHP -= damage;

        Debug.Log("Enemy: " + gameObject.name + " | Damage: " + damage);
        AudioManager.Instance.PlaySfx("EnemyHit");
        StartCoroutine(DamageEffect());

        if (m_EnemyCurrentHP <= 0)
        {
            m_EnemyCurrentHP = 0;
            DestroyEnemy();
        }
    }

    private void EvaluateState()
    {
        // Change State To
        if (AtkTarget != null)
        {
            this.ChangeState(AtkState);
        }
        else
        {
            // Chasing
            this.ChangeState(ChaseState);
        }
    }
    private void EnemyDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EnemyAtkRange, AtkLayerMask);
        if (colliders.Length > 0)
        {
            // Check if existing target is still within range
            if (Array.Exists<Collider2D>(colliders, collider => collider.transform == m_AtkTarget))
                return;

            // Get the closest target
            float nearestDist = float.PositiveInfinity;
            int targetIdx = 0;
            for (int i = 0; i < colliders.Length; i++)
            {
                float dist = Vector2.Distance(transform.position, colliders[i].transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    targetIdx = i;
                }
            }
            m_AtkTarget = colliders[targetIdx].transform;
        }
        else
        {
            m_AtkTarget = null;
        }
    }

    private void AdjustRotation()
    {
        if (m_AtkTarget != null)
            transform.rotation = Util.LookAt2DRotation(transform.position, m_AtkTarget.transform.position);
        else
            transform.rotation = Util.LookAt2DRotation(transform.position, LevelManager.Instance.Player.transform.position);
    }

    private IEnumerator DamageEffect()
    {
        // Set Active model
        if (m_Damaged)
            yield break;

        m_Damaged = true;
        m_DamagedModel.SetActive(true);

        // Disable damage model
        yield return new WaitForSeconds(DAMAGE_DURATION);

        if (m_Damaged)
        {
            m_DamagedModel.SetActive(false);
            m_Damaged = false;
        }
    }

    private void DestroyEnemy()
    {
        gameObject.SetActive(false);
        Essence essence = LevelManager.Instance.PoolManager.Essence.GetNextObject();
        essence.transform.position = transform.position;
        essence.InitializeObject(EssenceDropAmt);
        AudioManager.Instance.PlaySfx("EnemyDestroyed");

    }

}
