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
        this.m_TotalTimeLbl = this.Root.Q<Label>("total-time-lbl");
        this.m_EnemiesKilledLbl = this.Root.Q<Label>("enemies-killed-lbl");
        this.m_HighScoreLbl = this.Root.Q<Label>("high-score-lbl");

        this.m_QuitBtn = this.Root.Q<Button>("quit-btn");

        this.m_QuitBtn.clicked += () =>
        {
            GameManager.Instance.ToStart();
        };
    }
}
