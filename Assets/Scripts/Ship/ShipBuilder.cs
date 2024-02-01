using UnityEngine;
using Unity.Mathematics;

public class ShipBuilder : MonoBehaviour
{
    [Tooltip("Number of tiles counting from the left towards the center but not including the center tile.")]
    [SerializeField] private uint TilesFromCenter;

    [SerializeField] private TileNode TileNodePrefab;
    [SerializeField] private TileNode[] TileNodes;

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

    private void Awake()
    {
        int gridLength = (int)this.TilesFromCenter * 2 + 1;
        int tileCount = gridLength * gridLength;

        this.TileNodes = new TileNode[tileCount];

        // Create tile entities
        for (int y = 0; y < gridLength; y++)
        {
            for (int x = 0; x < gridLength; x++)
            {
                int2 index2D = new int2(x, y);
                int flattenIndex = mathx.flatten_int2(index2D, gridLength);

                TileNode tileNode = Object.Instantiate(this.TileNodePrefab, this.transform);

                // // Setup parent child relationship
                // shipGridChildren.Add(new Child { Value = tileEntity });
                // commands.AddComponent<Parent>(tileEntity, new Parent { Value = shipGridEntity });

                // // Add BuildTile
                // commands.AddComponent<BuildTile>(tileEntity, BuildTile.Default);

                // tileNodes[flattenIndex] = tileEntity;

                this.TileNodes[flattenIndex] = tileNode;
            }
        }
    }
}
