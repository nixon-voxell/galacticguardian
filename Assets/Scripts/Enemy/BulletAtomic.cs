using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtomic : MonoBehaviour
{
    [SerializeField] private Sprite m_PreHitSprite;
    [SerializeField] private Sprite m_HitSprite;
    [SerializeField] private Transform m_SpriteRendererTransform;
    [SerializeField] private float m_HitCleanupTime;
    [SerializeField] private float m_HitRadiusFix; // Scale radius by abit to adjust visual with colliders hit

    // To be passed over to bullet
    private float m_AOERadius;
    private float m_Damage;
    private float m_HitDelay;
    private LayerMask m_AtkLayers;

    // To be assigned on runtime
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_Owner;


    private void Awake()
    {
        m_SpriteRenderer = m_SpriteRendererTransform.GetComponent<SpriteRenderer>();
    }

    public void StartBullet(float aoeRange, float damage, float hitDelay, LayerMask atkLayers)
    {
        gameObject.SetActive(true);

        m_AOERadius = aoeRange;
        m_Damage = damage;
        m_HitDelay = hitDelay;
        m_AtkLayers = atkLayers;

        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        // Pre hit
        float preHitT = 0;
        float preHitSpriteSize = 0f;
        m_SpriteRenderer.sprite = m_PreHitSprite;

        while (preHitT < 1)
        {
            // Times sprite size by 2 as aoe is radius
            preHitSpriteSize = Mathf.Lerp(0, m_AOERadius, preHitT);
            m_SpriteRendererTransform.localScale = new Vector2(preHitSpriteSize * 2, preHitSpriteSize * 2);
            preHitT += Time.deltaTime * (1 / m_HitDelay);
            yield return null;
        }

        // Hit
        m_SpriteRenderer.sprite = m_HitSprite;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_AOERadius - m_HitRadiusFix, m_AtkLayers);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++) 
            {
                colliders[i].GetComponent<IDamageable>().OnDamage(m_Owner, m_Damage);
            }
        }

        yield return new WaitForSeconds(m_HitCleanupTime);
        ResetBullet();
    }

    private void ResetBullet()
    {
        m_SpriteRenderer.sprite = null;
        gameObject.SetActive(false);
    }

}
