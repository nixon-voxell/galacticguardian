using UnityEngine;
using UnityEngine.UIElements;

public class InGameHud : UiMono
{
    [SerializeField] private uint TowerBtnCount;

    public VisualElement TileBtnGrp;
    public VisualElement TowerBtnGrp;

    private Label m_EssenceLbl;
    private Button m_TileBtn;
    private Button m_TileBuildBtn;

    private Button[] m_TowerBtns;

    public Button CreateTileBtn(Vector3 worldPosition)
    {
        return this.CreateBtn(worldPosition, this.m_TileBtn, this.TileBtnGrp);
    }

    public Button CreateTileBuildBtn(Vector3 worldPosition)
    {
        return this.CreateBtn(worldPosition, this.m_TileBuildBtn, this.TileBtnGrp);
    }

    public Button CreateBtn(Vector3 worldPosition, Button buttonPrefab, VisualElement targetRoot)
    {
        Button buildBtn = new Button();
        buildBtn.RegisterCallback<NavigationSubmitEvent>((evt) =>
        {
            evt.StopPropagation();
        }, TrickleDown.TrickleDown);

        foreach (string className in buttonPrefab.GetClasses())
        {
            buildBtn.AddToClassList(className);
        }

        Vector2 position = RuntimePanelUtils.CameraTransformWorldToPanel(
            this.Root.panel, worldPosition, Camera.main
        );
        buildBtn.style.left = position.x;
        buildBtn.style.top = position.y;

        targetRoot.Add(buildBtn);

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
        this.m_TileBuildBtn = this.Root.Q<Button>("tile-build-btn");

        this.m_TowerBtns = new Button[this.TowerBtnCount];

        for (uint t = 0; t < this.TowerBtnCount; t++)
        {
            this.m_TowerBtns[t] = this.Root.Q<Button>($"tower{t}-btn");
        }
    }

    private void Update()
    {
        this.m_EssenceLbl.text = GameStat.Instance.EssenceCount.ToString();
    }
}
