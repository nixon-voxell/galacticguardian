using UnityEngine;

public class TileNode : MonoBehaviour
{
    public TileNode[] Neighbors;

    private void Awake()
    {
        this.Neighbors = new TileNode[4];
    }
}
