using UnityEngine;

public class UserInput : SingletonMono<UserInput>
{
    [SerializeField] private Vector2 m_Movement;
    public Vector2 Movement => this.m_Movement;

    [SerializeField] private bool m_Build;
    public bool Build => this.m_Build;

    private void Update()
    {
        this.m_Movement.x = Input.GetAxisRaw("Horizontal");
        this.m_Movement.y = Input.GetAxisRaw("Vertical");

        this.m_Build = Input.GetKeyUp(KeyCode.B);
    }
}
