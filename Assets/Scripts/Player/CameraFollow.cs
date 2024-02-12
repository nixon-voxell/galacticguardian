using UnityEngine;

public class CameraFollow : SingletonMono<CameraFollow>
{
    [HideInInspector] public Transform PlayerTransform;

    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private SpriteRenderer m_Background;

    private Material m_BgMaterial;
    private Vector2 m_MaterialOffset;
    private float m_PositionMultiplier;

    private void Start()
    {
        this.m_BgMaterial = this.m_Background.material;
        // Initial position is the offset
        this.m_MaterialOffset = this.m_BgMaterial.GetVector("_Position");
        this.m_PositionMultiplier = this.m_BgMaterial.GetFloat("_Size") * 0.5f;
    }

    private void Update()
    {
        if (this.PlayerTransform == null)
        {
            return;
        }

        this.transform.position = this.PlayerTransform.position + this.m_Offset;
        if (m_Background != null)
        {
            this.m_BgMaterial.SetVector(
            "_Position",
            new Vector2(
                this.transform.position.x,
                this.transform.position.y
            ) * this.m_PositionMultiplier + this.m_MaterialOffset
        );
        }

    }
}
