using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenu : UiMono
{
    void Start()
    {
        if (this.m_Doc == null)
        {
            UnityEngine.Debug.LogError("No Button Found!");
            return;
        }

        var PlayBtn = this.m_Doc.rootVisualElement.Q("play-btn") as Button;
        var RetryBtn = this.m_Doc.rootVisualElement.Q("retry-btn") as Button;
        var QuitBtn = this.m_Doc.rootVisualElement.Q("quit-btn") as Button;

        if (PlayBtn != null)
        {
            Debug.Log("Play Button Found!");
            PlayBtn.clicked += OnButtonClick;
        }

        if (RetryBtn != null)
        {
            Debug.Log("Retry Button Found!");
            RetryBtn.clicked += OnButtonClick;
        }

        if (QuitBtn != null)
        {
            Debug.Log("Quit Button Found!");
            QuitBtn.clicked += OnQuitButtonClick;
        }
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene("Player_Movement");
    }
    public void OnQuitButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
