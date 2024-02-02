using UnityEngine;

public class EnemyChase : State
{
    public Enemy Enemy;

    // Set at runtime
    private Transform m_Victim;
    private float m_Speed;


    private Transform m_EnemyTrans;
    private Rigidbody2D m_EnemyRb;

    private void Awake()
    {
        m_EnemyTrans = Enemy.transform;
        m_EnemyRb = Enemy.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_Victim != null)
        {
            Vector3 dir = m_Victim.transform.position - Enemy.transform.position;
            m_EnemyRb.velocity = dir.normalized * m_Speed;
        }
    }
    protected override void OnEnter()
    {
        m_Victim = GameManager.Instance.Player.transform;
        Enemy enemy = this.StateController as Enemy;
        m_Speed = enemy.EnemyMovementSpeed;
    }

    protected override void OnExit()
    {
        m_Victim = null;
        m_Speed = 0;
        m_EnemyRb.velocity = Vector2.zero;
    }
}
