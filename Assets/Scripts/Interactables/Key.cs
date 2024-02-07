using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private const int CHECK_EVERY_N_FRAME = 20;

    [SerializeField] private GameObject m_CollectParticle;
    [SerializeField] private GameObject m_SpriteRenderer;

    // Assign on runtime
    private bool m_IsCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !m_IsCollected)
        {
            GameStat.Instance.AddKey();
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
                Destroy(gameObject);
            }
        }
    }

}
