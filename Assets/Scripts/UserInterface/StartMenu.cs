using UnityEngine;
using UnityEngine.UIElements;

public class StartMenu : UiMono
{
    private Button m_PlayBtn;
    private Button m_QuitBtn;

    void Start()
    {
        this.m_PlayBtn = this.m_Doc.rootVisualElement.Q<Button>("play-btn");
        this.m_QuitBtn = this.m_Doc.rootVisualElement.Q<Button>("quit-btn");

        this.m_PlayBtn.clicked += GameManager.Instance.ToInGame;
        this.m_QuitBtn.clicked += () =>
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        };
    }
}
