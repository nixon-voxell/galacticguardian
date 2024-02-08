using UnityEngine;
using UnityEngine.UIElements;

public class InGameHud : UiMono
{
    [SerializeField] private Color m_SelectedBorderColor;
    [SerializeField] private Tower[] m_TowerPrefabs;

    public uint TowerBtnCount => (uint)this.m_TowerPrefabs.Length;
    public Tower SelectedTowerPrefab => this.m_TowerPrefabs[this.m_SelectedTower];

    public VisualElement TileBtnGrp;
    public VisualElement TowerBtnGrp;

    private Label m_EssenceLbl;
    private Label m_TimeLbl;
    private Button m_BuildTileBtn;
    private Button m_BuildTowerBtn;

    private Button[] m_TowerBtns;
    private uint m_SelectedTower;

    public uint SelectedTower => this.m_SelectedTower;

    private bool m_BuildMenuActive;

    public Button CreateBuildTileBtn(Vector3 worldPosition)
    {
        return this.CreateBtn(worldPosition, this.m_BuildTileBtn, this.TileBtnGrp);
    }

    public Button CreateBuildTowerBtn(Vector3 worldPosition)
    {
        return this.CreateBtn(worldPosition, this.m_BuildTowerBtn, this.TileBtnGrp);
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

    public void InitTowerSelection()
    {
        this.m_SelectedTower = 0;

        Util.SetBorderColor(this.m_TowerBtns[0], this.m_SelectedBorderColor);

        for (uint t = 1; t < this.TowerBtnCount; t++)
        {
            Util.SetBorderColor(this.m_TowerBtns[t], Color.white);
        }
    }

    public void ResetButtons()
    {
        this.InitTowerSelection();
        this.TileBtnGrp.Clear();
    }

    public void SetBuildMenuActive(bool active)
    {
        this.TileBtnGrp.visible = active;
        this.TileBtnGrp.style.display = active ? DisplayStyle.Flex : DisplayStyle.None;
        this.TowerBtnGrp.visible = active;

        this.m_BuildMenuActive = active;
    }

    protected override void Awake()
    {
        base.Awake();

        this.TileBtnGrp = this.Root.Q<VisualElement>("tile-btn-grp");
        this.TowerBtnGrp = this.Root.Q<VisualElement>("tower-btn-grp");

        this.m_TimeLbl = this.Root.Q<Label>("time-lbl");
        this.m_EssenceLbl = this.Root.Q<Label>("essence-lbl");
        this.m_BuildTileBtn = this.Root.Q<Button>("build-tile-btn");
        this.m_BuildTowerBtn = this.Root.Q<Button>("build-tower-btn");

        this.m_TowerBtns = new Button[this.TowerBtnCount];

        for (uint t = 0; t < this.TowerBtnCount; t++)
        {
            this.m_TowerBtns[t] = this.Root.Q<Button>($"tower{t}-btn");
            uint index = t;
            this.m_TowerBtns[t].clicked += () =>
            {
                Util.SetBorderColor(this.m_TowerBtns[this.m_SelectedTower], Color.white);
                this.m_SelectedTower = index;
                Util.SetBorderColor(this.m_TowerBtns[index], this.m_SelectedBorderColor);
            };
        }

        this.InitTowerSelection();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrGameState != GameState.InGame)
        {
            return;
        }

        this.m_EssenceLbl.text = GameStat.Instance.EssenceCount.ToString();
        int hrs, mins, secs;
        Util.CalculateTimeFromSeconds((int)GameStat.Instance.Time, out hrs, out mins, out secs);
        this.m_TimeLbl.text = $"{hrs:00}:{mins:00}:{secs:00}";

        if (UserInput.Instance.Build)
        {
            this.SetBuildMenuActive(!this.m_BuildMenuActive);
        }
    }
}
