using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtomic : MonoBehaviour
{
    [SerializeField] private Sprite m_PreHitSprite;
    [SerializeField] private Sprite m_HitSprite;
    [SerializeField] private Transform m_PreHitRendererTransform;
    [SerializeField] private Transform m_HitRendererTransform;
    [SerializeField] private float m_HitCleanupTime;
    [SerializeField] private float m_HitRadiusFix; // Scale radius by abit to adjust visual with colliders hit

    // To be passed over to bullet
    private float m_AOERadius;
    private float m_Damage;
    private float m_HitDelay;
    private LayerMask m_AtkLayers;

    // To be assigned on runtime
    private SpriteRenderer m_PreHitRenderer;
    private SpriteRenderer m_HitRenderer;
    private Transform m_Owner;


    private void Awake()
    {
        m_PreHitRenderer = m_PreHitRendererTransform.GetComponent<SpriteRenderer>();
        m_HitRenderer = m_HitRendererTransform.GetComponent<SpriteRenderer>();
    }

    public void StartBullet(float aoeRange, float damage, float hitDelay, LayerMask atkLayers)
    {
        gameObject.SetActive(true);

        m_AOERadius = aoeRange;
        m_Damage = damage;
        m_HitDelay = hitDelay;
        m_AtkLayers = atkLayers;

        m_PreHitRenderer.enabled = false;
        m_HitRenderer.gameObject.SetActive(false);

        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        // Pre hit
        float preHitT = 0;
        float preHitSpriteSize = 0f;
        m_PreHitRenderer.enabled = true;
        StartCoroutine(RenderOpacity(m_PreHitRenderer, m_HitDelay, true));

        while (preHitT < 1)
        {
            // Times sprite size by 2 as aoe is radius
            preHitSpriteSize = Mathf.Lerp(0, m_AOERadius, preHitT);
            m_PreHitRendererTransform.localScale = new Vector2(preHitSpriteSize * 2, preHitSpriteSize * 2);
            preHitT += Time.deltaTime * (1 / m_HitDelay);
            yield return null;
        }
        m_PreHitRenderer.enabled = false;

        // Hit
        m_HitRenderer.gameObject.SetActive(true);
        m_HitRendererTransform.localScale = new Vector2(m_AOERadius * 2, m_AOERadius * 2);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_AOERadius - m_HitRadiusFix, m_AtkLayers);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++) 
            {
                colliders[i].GetComponent<IDamageable>().OnDamage(m_Owner, m_Damage);
            }
        }
        StartCoroutine(RenderOpacity(m_HitRenderer, m_HitCleanupTime, false));

        yield return new WaitForSeconds(m_HitCleanupTime);
        ResetBullet();
    }

    private void ResetBullet()
    {
        m_PreHitRenderer.enabled = false;
        m_HitRenderer.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private IEnumerator RenderOpacity(SpriteRenderer renderer, float duration, bool increasingly)
    {
        if (increasingly)
        {
            float t = 0;
            while (t < 1)
            {
                Color color = renderer.color;
                color.a = t;
                renderer.color = color;
                t += Time.deltaTime * (1 / duration);
                yield return null;
            }
        }
        else
        {
            float t = 1;
            while (t > 0)
            {
                Color color = renderer.color;
                color.a = t;
                renderer.color = color;
                t -= Time.deltaTime * (1 / duration);
                yield return null;
            }
        }
    }
}
