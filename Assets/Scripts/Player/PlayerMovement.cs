using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    private float m_speedX, m_speedY;
    private Rigidbody2D rb;
    [SerializeField] private GameObject m_Explosion;
    [SerializeField] private GameObject m_Player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_speedX = Input.GetAxisRaw("Horizontal") * MovementSpeed;
        m_speedY = Input.GetAxisRaw("Vertical") * MovementSpeed;
        rb.velocity = new Vector2(m_speedX, m_speedY);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (m_Explosion != null) {
                Instantiate(m_Explosion, transform.position, Quaternion.identity);
                Destroy(m_Player);
            }
        }

        }
 }

