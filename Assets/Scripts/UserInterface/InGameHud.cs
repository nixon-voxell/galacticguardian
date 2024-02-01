using UnityEngine.UIElements;

public class InGameHud : UiMono
{
    private Button m_TileBtn;

    private void Start()
    {
        this.m_TileBtn = this.Root.Q<Button>("tile-btn");

        // Button newBtn = new Button();

        this.Root.Add(this.m_TileBtn);
        UnityEngine.Debug.Log(this.m_TileBtn.style.left);
        this.m_TileBtn.style.left = 100.0f;
    }
}
