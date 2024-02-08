using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(TileHealth))]
public class TileNode : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer => this.m_SpriteRenderer;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;

    private TileHealth m_TileHealth;
    public TileHealth TileHealth => this.m_TileHealth;

    public List<TileNode> Neighbors;
    public bool Connected = false;

    public Button BuildTileBtn;
    public Button BuildTowerBtn;

    private bool m_Active = false;
    public bool Active => this.m_Active;
    public bool m_CanBuildTile = false;
    public bool CanBuildTile => this.m_CanBuildTile;
    public bool m_CanBuildTower = false;
    public bool CanBuildTower => this.m_CanBuildTower;

    public Tower Tower;

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
        this.m_Active = active;
    }

    public void SetCanBuildTile(bool canBuild)
    {
        this.m_CanBuildTile = canBuild;
        this.BuildTileBtn.visible = canBuild;
    }

    public void SetCanBuildTower(bool canBuild)
    {
        this.m_CanBuildTower = canBuild;
        this.BuildTowerBtn.visible = canBuild;
    }

    public void DestroyTile()
    {
        if (this.Tower != null)
        {
            Object.Destroy(this.Tower.gameObject);
            this.Tower = null;
        }

        this.SetActive(false);
        LevelManager.Instance.Player.ShipBuilder.RecheckTileButtons();
    }

    public void SetColor(Color color)
    {
        this.SpriteRenderer.color = color;
    }

    public void Awake()
    {
        this.m_TileHealth = this.GetComponent<TileHealth>();
    }
}
