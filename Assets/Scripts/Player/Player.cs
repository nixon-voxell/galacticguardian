using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ShipBuilder m_ShipBuilder;
    public ShipBuilder ShipBuilder => this.m_ShipBuilder;

    private void Start()
    {
        CameraFollow.Instance.PlayerTransform = this.transform;
        LevelManager.Instance.Player = this;
    }

    private void OnDisable()
    {
        LevelManager.Instance.Player = null;
    }
}
