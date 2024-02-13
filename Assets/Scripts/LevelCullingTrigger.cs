using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCullingTrigger : MonoBehaviour
{
    [SerializeField] private LevelCulling m_LevelCulling;
    [SerializeField] private bool m_IsLoader;
    [SerializeField] private int m_LevelToLoadUnload;
    [SerializeField] private GameObject m_LevelDeloadBlocker; // On deload previous level, place a blocker to prevent going back

    private void Start()
    {
        if (m_LevelDeloadBlocker != null)
            m_LevelDeloadBlocker.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Tile"))
        {
            if (m_IsLoader)
            {
                m_LevelCulling.LoadLevel(m_LevelToLoadUnload);
            }
            else
            {
                m_LevelCulling.UnloadLevel(m_LevelToLoadUnload);
                m_LevelDeloadBlocker.SetActive(true);
            }
        }
    }
}
