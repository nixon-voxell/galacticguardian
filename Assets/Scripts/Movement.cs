using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2 m_MoveForceRange;
    [SerializeField] private bool m_WorldMovement;

    private Vector2 m_RandomDirection;
    private float m_MoveForce;

    private void Start()
    {
        m_RandomDirection = new Vector2(Random.Range(-1f, 1.0f), Random.Range(-1f, 1.0f));
        m_MoveForce = Random.Range(m_MoveForceRange.x, m_MoveForceRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_WorldMovement)
        {
            transform.Translate((m_RandomDirection * m_MoveForce * Time.deltaTime), Space.World);
        }
        else
        {
            transform.Translate(m_RandomDirection * m_MoveForce * Time.deltaTime);
        }
    }
}
