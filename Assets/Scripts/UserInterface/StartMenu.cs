using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenu : UiMono
{
   UnityEngine.UI.Button UiButton;

    void Start()
    {
        if (this.m_Doc == null)
        {
            UnityEngine.Debug.LogError("No Button Found!");
        }

        var visualElement = this.m_Doc.rootVisualElement.Q("play-btn");

        if (visualElement != null && visualElement.name == "play-btn")
        {
            Debug.Log("Button Found!");
            visualElement.RegisterCallback<ClickEvent>(OnButtonClick);
        }
        else
        {
            Debug.LogError("Element with name 'play-btn' is not found or is not a Button!");
        }
    }


    public void OnButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("Player_Movement");
    }
}
