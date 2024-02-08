using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private GameObject m_Explosion;
    [SerializeField] private GameObject m_Player;

    private Rigidbody2D m_RigidBody;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.m_RigidBody.velocity = UserInput.Instance.Movement * this.m_MovementSpeed;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_Explosion != null)
            {
                Instantiate(this.m_Explosion, transform.position, Quaternion.identity);
                Destroy(m_Player);
            }
        }

    }
}

