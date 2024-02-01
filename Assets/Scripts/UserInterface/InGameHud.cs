using UnityEngine;
using UnityEngine.UIElements;

public class InGameHud : UiMono
{
    private Button m_TileBtn;

    protected override void Awake()
    {
        base.Awake();

        this.m_TileBtn = this.Root.Q<Button>("tile-btn");
    }

    public Button CreateBuildBtn(Vector3 worldPosition)
    {
        Button buildBtn = new Button();

        foreach (string className in this.m_TileBtn.GetClasses())
        {
            buildBtn.AddToClassList(className);
        }

        Vector2 position = RuntimePanelUtils.CameraTransformWorldToPanel(
            this.Root.panel, worldPosition, Camera.main
        );
        buildBtn.style.left = position.x;
        buildBtn.style.top = position.y;

        this.Root.Add(buildBtn);

        return buildBtn;
    }
}
