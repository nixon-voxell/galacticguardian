using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ShipBuilder : MonoBehaviour
{
    [Tooltip("Number of tiles counting from the center towards the edge but not including the center tile.")]
    [SerializeField] private uint m_TilesFromCenter = 6;
    [SerializeField] private float m_TileSize = 0.5f;
    [SerializeField] private TileNode m_TileNodePrefab;
    [SerializeField] private Color m_CenterTileColor;

    [Tooltip("The number of essence that a single tile cost.")]
    [SerializeField] private uint m_EssenceCost = 100;
    [SerializeField] private float m_TileDefaultHealth = 100.0f;
    [SerializeField] private float m_TileCenterHealth = 1000.0f;

    [SerializeField] private Sprite m_CenterTileSprite;

    private TileNode[] m_TileNodes;
    private InGameHud m_InGameHud;

    private int m_GridLength;
    private int m_TileCount;
    private int2 m_MaxIndex2D;
    // Core tile index
    private int m_CenterTileIndex;

    private int2[] indexOffsets = new int2[]
    {
        // Left
        new int2(-1, 0),
        // Right
        new int2(1, 0),
        // Bottom
        new int2(0, -1),
        // Top
        new int2(0, 1),
    };

    public void RecheckTileButtons()
    {
        this.CheckTilesConnected();
        this.CheckCanBuildTiles();
        this.CheckCanBuildTowers();
    }

    private void Start()
    {
        this.m_InGameHud = UiManager.Instance.GetUi<InGameHud>();
        // Reinitialize tower selection index
        this.m_InGameHud.InitTowerSelection();

        this.m_GridLength = (int)this.m_TilesFromCenter * 2 + 1;
        this.m_TileCount = this.m_GridLength * this.m_GridLength;
        this.m_MaxIndex2D = this.m_GridLength - 1;
        this.m_CenterTileIndex = mathx.flatten_int2((int)this.m_TilesFromCenter, this.m_GridLength);

        float tileStart = -this.m_TileSize * (this.m_TilesFromCenter + 0.5f) + this.m_TileSize * 0.5f;

        this.m_TileNodes = new TileNode[this.m_TileCount];

        // Create tiles
        for (int y = 0; y < this.m_GridLength; y++)
        {
            for (int x = 0; x < this.m_GridLength; x++)
            {
                int2 index2D = new int2(x, y);
                int flattenIndex = mathx.flatten_int2(index2D, this.m_GridLength);

                Vector3 position = new Vector3(tileStart + x * this.m_TileSize, tileStart + y * this.m_TileSize, 0.0f);

                TileNode tileNode = Object.Instantiate(this.m_TileNodePrefab, position, Quaternion.identity, this.transform);

                // Build tile button
                tileNode.BuildTileBtn = this.m_InGameHud.CreateBuildTileBtn(position);
                tileNode.BuildTileBtn.clicked += () =>
                {
                    // Tile can only be built when there is enough essence
                    if (GameStat.Instance.EssenceCount < this.m_EssenceCost)
                    {
                        AudioManager.Instance.PlaySfx("BuildFail");
                        return;
                    }

                    GameStat.Instance.AddEssence(-(int)this.m_EssenceCost);
                    ShakerManager.Instance.Shake("TilePlaced");
                    AudioManager.Instance.PlaySfx("TileBuild");

                    tileNode.SetActive(true);
                    tileNode.TileHealth.InitializeHealth(this.m_TileDefaultHealth, this.m_TileDefaultHealth);

                    this.CheckCanBuildTiles();
                    this.CheckCanBuildTowers();
                };

                // Build tower button
                tileNode.BuildTowerBtn = this.m_InGameHud.CreateBuildTowerBtn(position);
                tileNode.BuildTowerBtn.clicked += () =>
                {
                    int cost = this.m_InGameHud.SelectedTowerPrefab.EssenceCost;
                    // Tower can only be built when there is enough essence
                    if (GameStat.Instance.EssenceCount < cost)
                    {
                        AudioManager.Instance.PlaySfx("BuildFail");
                        return;
                    }

                    tileNode.Tower = Object.Instantiate(this.m_InGameHud.SelectedTowerPrefab, tileNode.transform);
                    GameStat.Instance.AddEssence(-cost);
                    ShakerManager.Instance.Shake("TowerPlaced");
                    AudioManager.Instance.PlaySfx("TowerBuild");

                    tileNode.TileHealth.InitializeHealth(tileNode.Tower.TowerMaxHP, tileNode.Tower.TowerMaxHP);

                    this.CheckCanBuildTiles();
                    this.CheckCanBuildTowers();
                };

                this.m_TileNodes[flattenIndex] = tileNode;
            }
        }

        // Set tile neighbors
        for (int y = 0; y < this.m_GridLength; y++)
        {
            for (int x = 0; x < this.m_GridLength; x++)
            {
                int2 index2D = new int2(x, y);
                int flattenIndex = mathx.flatten_int2(index2D, this.m_GridLength);

                TileNode tileNode = this.m_TileNodes[flattenIndex];

                foreach (int2 offset in indexOffsets)
                {
                    int2 neighborIndex2D = index2D + offset;
                    if (
                        math.any(neighborIndex2D < 0) ||
                        math.any(neighborIndex2D > this.m_MaxIndex2D)
                    )
                    {
                        continue;
                    }

                    TileNode neighborTile = this.m_TileNodes[mathx.flatten_int2(neighborIndex2D, this.m_GridLength)];
                    tileNode.Neighbors.Add(neighborTile);
                }
            }
        }

        TileNode centerTile = this.GetCenterTile();
        // Set core tile to active
        centerTile.SetActive(true);
        // Set core tile color
        centerTile.SetTileSprite(m_CenterTileSprite);
        centerTile.SetColor(m_CenterTileColor);
        centerTile.BuildTowerBtn.style.backgroundColor = this.m_CenterTileColor;
        // Set health
        centerTile.TileHealth.InitializeHealth(this.m_TileCenterHealth, this.m_TileCenterHealth);

        this.CheckTilesConnected();
        this.CheckCanBuildTiles();
        this.CheckCanBuildTowers();

    }

    private void Update()
    {
        if (this.GetCenterTile().Active == false)
        {
            GameManager.Instance.ToEnd();
        }
    }

    private void CheckCanBuildTiles()
    {
        // Allow tile for building based on neighbor activeness
        foreach (TileNode tileNode in this.m_TileNodes)
        {
            if (tileNode.Active)
            {
                tileNode.SetCanBuildTile(false);
                continue;
            }

            bool neighborActive = false;

            foreach (TileNode neighborNode in tileNode.Neighbors)
            {
                if (neighborNode.Active)
                {
                    neighborActive = true;
                    break;
                }
            }

            tileNode.SetCanBuildTile(neighborActive);
        }
    }

    private void CheckCanBuildTowers()
    {
        foreach (TileNode tileNode in this.m_TileNodes)
        {
            if (tileNode.Active == false)
            {
                tileNode.SetCanBuildTower(false);
                continue;
            }

            if (tileNode.Tower != null)
            {
                tileNode.SetCanBuildTower(false);
                continue;
            }

            tileNode.SetCanBuildTower(true);
        }

        // Disable building in center tile
        this.GetCenterTile().SetCanBuildTower(false);
    }

    private void CheckTilesConnected()
    {
        // Set all Connected to false first
        foreach (TileNode tileNode in this.m_TileNodes)
        {
            tileNode.Connected = false;
        }


        TileNode centerTile = this.GetCenterTile();
        centerTile.Connected = true;
        this.RecursiveTraverseForConnectivity(in centerTile.Neighbors);

        // Deactivate all disconnected tile
        foreach (TileNode tileNode in this.m_TileNodes)
        {
            if (tileNode.Connected == false)
            {
                tileNode.SetActive(false);
            }
        }
    }

    private void RecursiveTraverseForConnectivity(in List<TileNode> tileNodes)
    {
        foreach (TileNode tileNode in tileNodes)
        {
            // Connection only works on enabled tiles
            if (tileNode.Active == false || tileNode.Connected == true)
            {
                continue;
            }

            tileNode.Connected = true;
            this.RecursiveTraverseForConnectivity(in tileNode.Neighbors);
        }
    }

    private TileNode GetCenterTile()
    {
        return this.m_TileNodes[this.m_CenterTileIndex];
    }
}
