using UnityEngine;
using UnityEngine.UIElements;

public class InGameHud : UiMono
{
    private Label m_EssenceLbl;
    public VisualElement TileBtnGrp;
    private Button m_TileBtn;

    public Button CreateBuildBtn(Vector3 worldPosition)
    {
        Button buildBtn = new Button();
        buildBtn.RegisterCallback<NavigationSubmitEvent>((evt) =>
        {
            evt.StopPropagation();
        }, TrickleDown.TrickleDown);

        foreach (string className in this.m_TileBtn.GetClasses())
        {
            buildBtn.AddToClassList(className);
        }

        Vector2 position = RuntimePanelUtils.CameraTransformWorldToPanel(
            this.Root.panel, worldPosition, Camera.main
        );
        buildBtn.style.left = position.x;
        buildBtn.style.top = position.y;

        this.TileBtnGrp.Add(buildBtn);

        return buildBtn;
    }

    protected override void Awake()
    {
        base.Awake();

        this.m_EssenceLbl = this.Root.Q<Label>("essence-lbl");
        this.TileBtnGrp = this.Root.Q<VisualElement>("tile-btn-grp");
        this.m_TileBtn = this.Root.Q<Button>("tile-btn");
    }

    private void Update()
    {
        this.m_EssenceLbl.text = GameStat.Instance.EssenceCount.ToString();
    }
}
