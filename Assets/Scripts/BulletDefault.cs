using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDefault : MonoBehaviour
{
    [SerializeField] private string m_HitFx; //Hit effect name on the fx manager SO
    [SerializeField] private GameObject m_Pfx; 
    [SerializeField] private SpriteRenderer m_MeshRenderer;
    [SerializeField] private Collider2D m_Collider;

    private BulletStat m_BulletStat;
    
    private bool m_Activated = false;
    private Rigidbody2D m_Rb;

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

        this.m_Activated = true;
        this.m_Collider.enabled = true;
        this.m_MeshRenderer.enabled = true;
        if (m_Pfx != null)
            this.m_Pfx?.SetActive(true);

        Invoke("ResetBullet", this.m_BulletStat.BulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable victim;
        if (collision.collider.TryGetComponent<IDamageable>(out victim))
        {
            victim.OnDamage(transform, m_BulletStat.BulletDamage);

            if (!m_BulletStat.PierceThroughEnemy)
                m_BulletStat.EnemyGoThroughCount--;

            // TODO: SOUNDFX - Turret bullet hit sfx
        }

        //if (m_HitFx != "")
        //    GameManager.Instance.EffectsManager.PlayEffectAtLocation(m_HitFx, collision.contacts[0].point);

        // Enemy go through count will be -1 for it to be resetted
        if (m_BulletStat.EnemyGoThroughCount < 0 && !m_BulletStat.PierceThroughEnemy)
            ResetBullet();
    }
    private void ResetBullet()
    {
        if (m_Activated)
        {
            m_Activated = false;
            m_BulletStat.BulletSpeed = 0;
            m_Collider.enabled = false;
            m_MeshRenderer.enabled = false;
            if (m_Pfx != null)
                m_Pfx.SetActive(false);
        }
    }
}
 