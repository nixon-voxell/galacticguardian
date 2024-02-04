using UnityEngine;

public class CameraFollow : SingletonMono<CameraFollow>
{
    [HideInInspector] public Transform PlayerTransform;

    [SerializeField] private Vector3 m_Offset;

    private void Update()
    {
        if (this.PlayerTransform == null)
        {
            return;
        }

        this.transform.position = this.PlayerTransform.position + this.m_Offset;
    }
}
