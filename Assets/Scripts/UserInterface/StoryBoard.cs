using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Slide
{
    public Texture2D Image;
    [TextArea] public string Content;
}

public class StoryBoard : UiMono
{
    [Voxell.Util.Scene] public string GameWorld;
    [SerializeField] private GameObject m_Grid;

    [SerializeField] private Slide[] m_Slides;
    // [SerializeField] private Texture2D[] m_EndTextures;

    private VisualElement m_Image;
    private Label m_SubtitleLbl;
    private Button m_NextBtn;

    private int m_StartTexIndex;
    private int m_EndTexIndex;

    private void Start()
    {
        this.m_Image = this.Root.Q<VisualElement>("image");
        this.m_SubtitleLbl = this.Root.Q<Label>("subtitle-lbl");
        this.m_NextBtn = this.Root.Q<Button>("next-btn");

        Slide slide = this.m_Slides[0];
        this.m_Image.style.backgroundImage = slide.Image;
        this.m_SubtitleLbl.text = slide.Content;

        this.m_NextBtn.clicked += () =>
        {
            if (GameManager.Instance.CurrGameState == GameState.InGame)
            {
                this.m_StartTexIndex = (this.m_StartTexIndex + 1) % this.m_Slides.Length;

                Slide slide = this.m_Slides[this.m_StartTexIndex];
                this.m_Image.style.backgroundImage = slide.Image;
                this.m_SubtitleLbl.text = slide.Content;


                if (this.m_StartTexIndex == 0)
                {
                    // Load game world
                    SceneManager.LoadSceneAsync(this.GameWorld, LoadSceneMode.Additive);
                    m_Grid.SetActive(true);

                    // Reset camera position
                    CameraFollow.Instance.transform.position = CameraFollow.Instance.Offset;

                    // Enable only in game hud
                    UiManager.Instance.SetOnlyVisible<InGameHud>();

                    InGameHud hud = UiManager.Instance.GetUi<InGameHud>();
                    hud.ResetButtons();
                    hud.SetBuildMenuActive(false);

                    // Reset Game Stat
                    GameStat.Instance.Reset();

                    // Return to normal time scale
                    Time.timeScale = 1.0f;

                    // SFX
                    AudioManager.Instance.PlaySfx("GameStart");
                    AudioManager.Instance.PlaySfx("Commander");
                }
            }
            // else if (GameManager.Instance.CurrGameState == GameState.End)
            // {
            //     this.m_Image.style.backgroundImage = this.m_EndTextures[this.m_EndTexIndex];
            //     this.m_EndTexIndex = (this.m_EndTexIndex + 1) % this.m_EndTextures.Length;

            //     if (this.m_EndTexIndex == this.m_EndTextures.Length - 1)
            //     {
            //     }
            // }
        };
    }
}
