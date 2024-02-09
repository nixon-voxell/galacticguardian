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

    public void ToStart()
    {
        // Unload game world
        SceneManager.UnloadSceneAsync(this.GameWorld);

        // Enable only start menu
        UiManager.Instance.SetOnlyVisible<StartMenu>();
        UiManager.Instance.GetUi<InGameHud>().ResetButtons();

        // Return to normal time scale
        Time.timeScale = 1.0f;

        this.m_CurrGameState = GameState.Start;
    }

    public void ToInGame()
    {
        // Load game world
        SceneManager.LoadSceneAsync(this.GameWorld, LoadSceneMode.Additive);

        // Reset camera position
        Camera.main.transform.position = Vector3.zero;

        // Enable only in game hud
        UiManager.Instance.SetOnlyVisible<InGameHud>();

        InGameHud hud = UiManager.Instance.GetUi<InGameHud>();
        hud.ResetButtons();
        hud.SetBuildMenuActive(true);

        // Reset Game Stat
        GameStat.Instance.Reset();

        // Return to normal time scale
        Time.timeScale = 1.0f;

        this.m_CurrGameState = GameState.InGame;
    }

    public void ToEnd()
    {

        // Enable only score board
        UiManager.Instance.SetOnlyVisible<ScoreBoard>();
        UiManager.Instance.GetUi<ScoreBoard>().SetValues((int)GameStat.Instance.Time, GameStat.Instance.KillCount);
        UiManager.Instance.GetUi<InGameHud>().SetBuildMenuActive(false);

        // Immediate slow down of time
        Time.timeScale = 0.1f;

        this.m_CurrGameState = GameState.End;
    }
}
