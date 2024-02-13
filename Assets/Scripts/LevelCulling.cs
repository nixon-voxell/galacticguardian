using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCulling : MonoBehaviour
{
    [SerializeField] private GameObject m_Level1;
    [SerializeField] private GameObject m_Level2;
    [SerializeField] private GameObject m_Level3;

    private void Start()
    {
        m_Level2.SetActive(false);
        m_Level3.SetActive(false);
    }

    public void LoadLevel(int level)
    {
        switch(level)
        {
            case 1: m_Level1.SetActive(true); break;
            case 2: m_Level2.SetActive(true); break;
            case 3: m_Level3.SetActive(true); break;
        }
    }

    public void UnloadLevel(int level)
    {
        switch (level)
        {
            case 1: m_Level1.SetActive(false); break;
            case 2: m_Level2.SetActive(false); break;
            case 3: m_Level3.SetActive(false); break;
        }
    }
}
