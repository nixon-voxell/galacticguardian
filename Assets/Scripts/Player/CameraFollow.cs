using UnityEngine;

public class CameraFollow : SingletonMono<CameraFollow>
{
    [HideInInspector] public Transform PlayerTransform;

    [SerializeField] private Vector3 m_Offset;
    [SerializeField] private Vector2 m_MaterialOffset;
    [SerializeField] private SpriteRenderer m_GridBackground;
    [SerializeField] private Transform m_Background;
    [SerializeField] private float m_BG1ParallaxFactor;
    [SerializeField] private Transform m_Background2;
    [SerializeField] private float m_BG2ParallaxFactor;   
    [SerializeField] private Transform m_Background3;
    [SerializeField] private float m_BG3ParallaxFactor;


    private Vector2 m_PreviousBGPosition;
    
    private void Start()
    {
        m_PreviousBGPosition = transform.position;
    }

    private void Update()
    {
        if (this.PlayerTransform == null)
        {
            return;
        }

        this.transform.position = this.PlayerTransform.position + this.m_Offset;
        if (m_GridBackground != null)
        {
            this.m_GridBackground.material.SetVector(
            "_Position",
            new Vector2(
                this.transform.position.x,
                this.transform.position.y
            ) * 0.5f + this.m_MaterialOffset
            );
        }


        // Background parallax

        if (transform.position.x != m_PreviousBGPosition.x || transform.position.y != m_PreviousBGPosition.y)
        {
            Vector2 translatedAmt = m_PreviousBGPosition - (Vector2) transform.position;

            m_Background.Translate(translatedAmt * m_BG1ParallaxFactor);
            m_Background2.Translate(translatedAmt * m_BG2ParallaxFactor);
            m_Background3.Translate(translatedAmt * m_BG3ParallaxFactor);

            m_PreviousBGPosition = transform.position;
        }

        
    }
}
