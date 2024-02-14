using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    InGame,
    End,
}

public class GameManager : SingletonMono<GameManager>
{
    public GameState CurrGameState => this.m_CurrGameState;
    private GameState m_CurrGameState = GameState.Start;

    [Voxell.Util.Scene] public string GameWorld;
    [SerializeField] private GameObject m_Grid;

    public void ToStart()
    {
        this.m_Grid.SetActive(false);

        // Reset camera position
        CameraFollow.Instance.transform.position = CameraFollow.Instance.Offset;

        // Unload game world
        SceneManager.UnloadSceneAsync(this.GameWorld);

        // Enable only start menu
        UiManager.Instance.SetOnlyVisible<StartMenu>();
        UiManager.Instance.GetUi<InGameHud>().ResetButtons();
        UiManager.Instance.GetUi<ScoreBoard>().HideBestScore();

        // Return to normal time scale
        Time.timeScale = 1.0f;

        this.m_CurrGameState = GameState.Start;
    }

    public void ToInGame()
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

        this.m_CurrGameState = GameState.InGame;
    }

    public void ToEnd(bool win = false)
    {
        if (m_CurrGameState == GameState.InGame)
        {
            // Disable build menu
            UiManager.Instance.GetUi<InGameHud>().SetBuildMenuActive(false);

            // Enable only score board
            UiManager.Instance.SetOnlyVisible<ScoreBoard>();
            ScoreBoard scoreBoard = UiManager.Instance.GetUi<ScoreBoard>();

            scoreBoard.SetValues((int)GameStat.Instance.Time, GameStat.Instance.KillCount);
            if (win)
            {
                scoreBoard.SetTitle("You Win!", Color.green);
            }
            else
            {
                scoreBoard.SetTitle("Game Over", Color.red);
            }

            // Immediate slow down of time
            Time.timeScale = 0.1f;

            this.m_CurrGameState = GameState.End;
        }
    }
}
