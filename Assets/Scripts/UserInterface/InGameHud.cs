using UnityEngine;
using UnityEngine.UIElements;

public class InGameHud : UiMono
{
    public VisualElement TileBtnGrp;
    public VisualElement TowerBtnGrp;

    private Label m_EssenceLbl;
    private Button m_TileBtn;

    private Button m_Tower0Btn;
    private Button m_Tower1Btn;
    private Button m_Tower2Btn;

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

        this.TileBtnGrp = this.Root.Q<VisualElement>("tile-btn-grp");
        this.TowerBtnGrp = this.Root.Q<VisualElement>("tower-btn-grp");
        this.TowerBtnGrp.SetEnabled(false);

        this.m_EssenceLbl = this.Root.Q<Label>("essence-lbl");
        this.m_TileBtn = this.Root.Q<Button>("tile-btn");

        this.m_Tower0Btn = this.Root.Q<Button>("tower0-btn");
        this.m_Tower1Btn = this.Root.Q<Button>("tower1-btn");
        this.m_Tower2Btn = this.Root.Q<Button>("tower2-btn");

        this.m_Tower0Btn.clicked += () =>
        {
            Debug.Log("Tower 0 Clicked");
        };
        this.m_Tower1Btn.clicked += () =>
        {
            Debug.Log("Tower 1 Clicked");
        };
        this.m_Tower2Btn.clicked += () =>
        {
            Debug.Log("Tower 2 Clicked");
        };
    }

    private void Update()
    {
        this.m_EssenceLbl.text = GameStat.Instance.EssenceCount.ToString();
    }
}
