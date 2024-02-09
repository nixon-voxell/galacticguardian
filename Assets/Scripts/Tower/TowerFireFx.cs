using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFireFx : MonoBehaviour
{
    [SerializeField] private Transform m_FirePfx;
    [SerializeField][Range(0,1.0f)] private float m_MinHPStartFirePerc;
    [SerializeField] private Vector2 m_FireScaleRange;

    private TileHealth m_TileHealth;
    private const int CHECK_EVERY_N_FRAME = 20;

    private void Awake()
    {
        m_TileHealth = GetComponent<TileHealth>();
        m_FirePfx.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Time.renderedFrameCount % CHECK_EVERY_N_FRAME == 0)
        {
            float tileHPPerc = m_TileHealth.Health / m_TileHealth.MaxHealth;

            // Lower than min start fire hp - Start fire
            if (tileHPPerc <= m_MinHPStartFirePerc)
            {
                if (!m_FirePfx.gameObject.activeSelf)
                    m_FirePfx.gameObject.SetActive(true);

                float scaledFire = Mathf.Lerp(m_FireScaleRange.y, m_FireScaleRange.x, tileHPPerc);
                m_FirePfx.localScale = new Vector2(scaledFire, scaledFire);
            }
            // Higher than min start fire hp but fire is active
            else if (m_FirePfx.gameObject.activeSelf)
                m_FirePfx.gameObject.SetActive(false);
        }
    }

}
