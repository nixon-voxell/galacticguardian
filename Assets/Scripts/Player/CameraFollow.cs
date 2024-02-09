using UnityEngine;

public class CameraFollow : SingletonMono<CameraFollow>
{
    [HideInInspector] public Transform PlayerTransform;

    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private Vector2 m_MaterialOffset;
    [SerializeField] private SpriteRenderer m_Background;

    private void Update()
    {
        if (this.PlayerTransform == null)
        {
            return;
        }

        this.transform.position = this.PlayerTransform.position + this.m_Offset;
        this.m_Background.material.SetVector(
            "_Position",
            new Vector2(
                this.transform.position.x,
                this.transform.position.y
            ) * 0.5f + this.m_MaterialOffset
        );
    }
}
