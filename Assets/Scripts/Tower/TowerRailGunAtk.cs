using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TowerRailGunAtk : MonoBehaviour, ITower
{
    [SerializeField] private float m_ShootWidth;
    [SerializeField] private float m_ShootRange;
    [SerializeField] private float m_ProjectileFxDuration;

    private Tower m_Tower;
    private Transform m_Target;
    private float m_NextAtkTime;
    private LineRenderer m_LineRenderer;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        ShootEnemy();
    }


    private void ShootEnemy()
    {
        if (m_Target != null && Time.time > m_NextAtkTime)
        {
            Vector2 dir = (m_Target.transform.position - transform.position).normalized;
            Vector2 endPt = (Vector2)transform.position + (dir * m_ShootRange);

            // Damage
            RaycastHit2D[] hit = Physics2D.LinecastAll(transform.position, endPt, m_Tower.TowerAtkLayers);
            if (hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    hit[i].transform.GetComponent<IDamageable>().OnDamage(transform, m_Tower.TowerDamage);
                }
            }

            // Visual Fx
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, endPt);
            StartCoroutine(ProjectFxFade());

            m_NextAtkTime = Time.time + (1 / m_Tower.TowerAtkRate);
        }
    }

    private IEnumerator ProjectFxFade()
    {
        
        float alpha = 1;
        float alphaDecreaseRate = 1 / m_ProjectileFxDuration;

        do
        {
            SetProjectileFxOpacity(alpha);
            alpha -= alphaDecreaseRate * Time.deltaTime;
            yield return null;
        } while (alpha > 0);
        SetProjectileFxOpacity(0);

    }

    private void SetProjectileFxOpacity(float alpha)
    {
        Color startColor = m_LineRenderer.startColor;
        Color endColor = m_LineRenderer.endColor;
        startColor.a = alpha;
        endColor.a = alpha;
        m_LineRenderer.startColor = startColor;
        m_LineRenderer.endColor = endColor;
    }

    public void SetNewTarget(Tower tower, Transform victim)
    {
        m_Target = victim;
    }

    public void SetTargetLost(Tower tower, Transform victim)
    {
        m_Target = null;
    }

    public void InitializeBehaviour(Tower tower)
    {
        m_Tower = tower;
        m_LineRenderer.startWidth = m_ShootWidth;
        m_LineRenderer.endWidth = m_ShootWidth;

        SetProjectileFxOpacity(0);

    }
}
