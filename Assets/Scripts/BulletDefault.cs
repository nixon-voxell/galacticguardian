using System.Collections;
using UnityEngine;

public class BulletDefault : MonoBehaviour
{
    [SerializeField] private GameObject m_Pfx;
    [SerializeField] private GameObject m_Renderer;
    [SerializeField] private Collider2D m_Collider;

    private BulletStat m_BulletStat;

    private bool m_Activated = false;
    private Rigidbody2D m_Rb;
    private Coroutine m_ResetCr;

    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_Activated)
        {
            m_Rb.velocity = transform.right * m_BulletStat.BulletSpeed;
        }
        else if (!m_Activated && m_Rb.velocity != Vector2.zero)
        {
            m_Rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Put enemyGoThroughCount = -1 to pierce through enemy
    /// </summary>
    public void StartBullet(BulletStat bulletStat)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        this.m_BulletStat = bulletStat;

        if (this.m_BulletStat.EnemyGoThroughCount == -1)
            this.m_BulletStat.PierceThroughEnemy = true;

        this.m_Rb.velocity = Vector2.zero;
        this.m_Activated = true;
        this.m_Collider.enabled = true;
        this.m_Renderer.SetActive(true);
        if (m_Pfx != null)
            this.m_Pfx?.SetActive(true);

        m_ResetCr = StartCoroutine(ResetBullet(m_BulletStat.BulletLifetime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable victim;
        if (collision.TryGetComponent<IDamageable>(out victim))
        {
            victim.OnDamage(transform, m_BulletStat.BulletDamage);

            if (!m_BulletStat.PierceThroughEnemy)
                m_BulletStat.EnemyGoThroughCount--;

        }

        Vector2 hitPos = collision.ClosestPoint(transform.position);
        PoolManager poolManager =  LevelManager.Instance.PoolManager;
        poolManager.PlacePoolItemAt(hitPos, poolManager.FxDemonBulletHit);

        // Enemy go through count will be -1 for it to be resetted
        if (m_BulletStat.EnemyGoThroughCount < 0 && !m_BulletStat.PierceThroughEnemy)
        {
            StopCoroutine(m_ResetCr);
            StartCoroutine(ResetBullet(0));
        }
    }

    private IEnumerator ResetBullet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // TODO: Why no reset???
        if (m_Activated)
        {
            m_Activated = false;
            m_BulletStat.BulletSpeed = 0;
            m_Collider.enabled = false;
            m_Renderer.SetActive(false);
            gameObject.SetActive(false);

            if (m_Pfx != null)
                m_Pfx.SetActive(false);
        }
    }
}

