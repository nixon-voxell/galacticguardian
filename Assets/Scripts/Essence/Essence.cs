using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Two modes available
/// Spawn On Start TRUE - Manually placed Essence in the game world
/// Spawn On Start FALSE - Spawned by enemies when they die
/// </summary>
public class Essence : MonoBehaviour
{
    private const int CHECK_EVERY_N_FRAME = 20;

    [SerializeField] private LayerMask m_PlayerLayer;
    [SerializeField] private GameObject m_CollectParticle;
    [SerializeField] private GameObject m_SpriteRenderer;

    // Assign on runtime
    private bool m_IsCollected = false;
    private int m_EssenceAmt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !m_IsCollected) 
        {
            GameStat.Instance.AddEssence(m_EssenceAmt);
            m_CollectParticle.SetActive(true);
            m_SpriteRenderer.SetActive(false);
            m_IsCollected = true;
        }
    }

    private void Update()
    {
        if (m_IsCollected && Time.renderedFrameCount % CHECK_EVERY_N_FRAME == 0)
        {
            if (!m_CollectParticle.activeSelf)
            {
                ResetObject();
            }
        }
    }

    public void InitializeObject(int essenceAmt)
    {
        m_EssenceAmt = essenceAmt;
        gameObject.SetActive(true);
        m_SpriteRenderer.SetActive(true);
        m_IsCollected = false;
    }

    private void ResetObject()
    {
        gameObject.SetActive(false);
        m_CollectParticle.SetActive(false);
        m_SpriteRenderer.SetActive(false);
        m_IsCollected = false;
    }
}
