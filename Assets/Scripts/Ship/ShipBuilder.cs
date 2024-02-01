using UnityEngine;
using Unity.Mathematics;

public class ShipBuilder : MonoBehaviour
{
    [Tooltip("Number of tiles counting from the center towards the edge but not including the center tile.")]
    [SerializeField] private uint TilesFromCenter;
    [SerializeField] private float TileSize = 1.0f;

    [SerializeField] private TileNode TileNodePrefab;
    [SerializeField] private TileNode[] TileNodes;

    private InGameHud InGameHud;

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

    private void Start()
    {
        this.InGameHud = UiManager.Instance.GetUi<InGameHud>();

        int gridLength = (int)this.TilesFromCenter * 2 + 1;
        int tileCount = gridLength * gridLength;
        int2 maxIndex2D = gridLength - 1;

        float tileStart = -this.TileSize * (this.TilesFromCenter + 0.5f);

        this.TileNodes = new TileNode[tileCount];

        // Create tiles
        for (int y = 0; y < gridLength; y++)
        {
            for (int x = 0; x < gridLength; x++)
            {
                int2 index2D = new int2(x, y);
                int flattenIndex = mathx.flatten_int2(index2D, gridLength);

                Vector3 position = new Vector3(tileStart + x * this.TileSize, tileStart + y * this.TileSize, 0.0f);

                TileNode tileNode = Object.Instantiate(this.TileNodePrefab, position, Quaternion.identity, this.transform);
                this.InGameHud.CreateBuildBtn(position);

                this.TileNodes[flattenIndex] = tileNode;
            }
        }

        // Set tile components
        for (int y = 0; y < gridLength; y++)
        {
            for (int x = 0; x < gridLength; x++)
            {
                int2 index2D = new int2(x, y);
                int flattenIndex = mathx.flatten_int2(index2D, gridLength);

                TileNode tileNode = this.TileNodes[flattenIndex];

                foreach (int2 offset in indexOffsets)
                {
                    int2 neighborIndex2D = index2D + offset;
                    if (
                        math.any(neighborIndex2D < 0) ||
                        math.any(neighborIndex2D > maxIndex2D)
                    )
                    {
                        continue;
                    }

                    TileNode neighborTile = this.TileNodes[mathx.flatten_int2(neighborIndex2D, gridLength)];
                    tileNode.Neighbors.Add(neighborTile);
                }
            }
        }

    }
}
