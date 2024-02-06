using UnityEngine.UIElements;

public class ScoreBoard : UiMono
{
    private Label m_TotalTimeLbl;
    private Label m_EnemiesKilledLbl;
    private Label m_HighScoreLbl;

    private Button m_QuitBtn;
    private Button m_RetryBtn;

    private void Start()
    {
        this.m_TotalTimeLbl = this.Root.Q<Label>();
        this.m_EnemiesKilledLbl = this.Root.Q<Label>();
        this.m_HighScoreLbl = this.Root.Q<Label>();

        this.m_QuitBtn = this.Root.Q<Button>();
        this.m_RetryBtn = this.Root.Q<Button>();
    }
}
