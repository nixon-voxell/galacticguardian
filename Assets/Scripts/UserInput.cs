using UnityEngine;

public class UserInput : SingletonMono<UserInput>
{
    private Vector2 m_Movement;
    [SerializeField] public Vector2 Movement => this.m_Movement;

    private void Update()
    {
        m_Movement.x = Input.GetAxisRaw("Horizontal");
        m_Movement.y = Input.GetAxisRaw("Vertical");
    }
}
