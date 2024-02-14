using UnityEngine;
using UnityEngine.UIElements;

public class ScoreBoard : UiMono
{
    private Label m_TitleLbl;
    private Label m_TotalTimeLbl;
    private Label m_EnemiesKilledLbl;
    private Label m_HighScoreLbl;
    private Label m_BestScoreLbl;
    private Label m_BestScoreIndicator;

    private Button m_QuitBtn;
    private Button m_RetryBtn;

    public void SetTitle(string title, Color color)
    {
        this.m_TitleLbl.text = title;
        this.m_TitleLbl.style.color = color;
    }

    public void SetValues(int time, int enemiesKilled)
    {
        int hrs, mins, secs;
        Util.CalculateTimeFromSeconds((int)GameStat.Instance.Time, out hrs, out mins, out secs);
        this.m_TotalTimeLbl.text = $"{hrs:00}:{mins:00}:{secs:00}";
        this.m_EnemiesKilledLbl.text = enemiesKilled.ToString();

        // Calculate score
        float timeScore = GameStat.Instance.Time * 10;
        float killScore = enemiesKilled * 50;
        int finalScore = (int)(timeScore + killScore);
        m_HighScoreLbl.text = finalScore.ToString();
        m_BestScoreIndicator.visible = false;

        float bestScore = PlayerPrefs.GetFloat("BestScore");

        if (finalScore > bestScore)
        {
            m_BestScoreIndicator.visible = true;
            bestScore = finalScore;
            PlayerPrefs.SetFloat("BestScore", finalScore);
        }

        m_BestScoreLbl.text = bestScore.ToString();
    }

    public void HideBestScore()
    {
        this.m_BestScoreIndicator.visible = false;
    }

    private void Start()
    {
        this.m_TitleLbl = this.Root.Q<Label>("title-lbl");
        this.m_TotalTimeLbl = this.Root.Q<Label>("total-time-lbl");
        this.m_EnemiesKilledLbl = this.Root.Q<Label>("enemies-killed-lbl");
        this.m_HighScoreLbl = this.Root.Q<Label>("high-score-lbl");
        this.m_BestScoreLbl = this.Root.Q<Label>("best-score-lbl");
        this.m_BestScoreIndicator = this.Root.Q<Label>("best-score-indicator");

        this.m_QuitBtn = this.Root.Q<Button>("quit-btn");

        this.m_QuitBtn.clicked += () =>
        {
            GameManager.Instance.ToStart();
        };
    }
}
