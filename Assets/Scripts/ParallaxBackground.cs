using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Attach this to the camera
/// </summary>
public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform m_Background1;
    [SerializeField] private Transform m_Background2;
    [SerializeField] private Transform m_Background3;
    [SerializeField] private float m_BG1ParallaxFactor;
    [SerializeField] private float m_BG2ParallaxFactor;
    [SerializeField] private float m_BG3ParallaxFactor;

    private Vector2 m_PreviousBGPosition;

    private void Start()
    {
        m_PreviousBGPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.x != m_PreviousBGPosition.x || transform.position.y != m_PreviousBGPosition.y)
        {
            Vector2 translatedAmt = m_PreviousBGPosition - (Vector2)transform.position;

            m_Background1.Translate(translatedAmt * m_BG1ParallaxFactor);
            m_Background2.Translate(translatedAmt * m_BG2ParallaxFactor);
            m_Background3.Translate(translatedAmt * m_BG3ParallaxFactor);

            m_PreviousBGPosition = transform.position;
        }
    }
}
