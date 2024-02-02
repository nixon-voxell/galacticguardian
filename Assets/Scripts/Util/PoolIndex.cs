using UnityEngine;

public class PoolIndex : MonoBehaviour
{
    private int m_Index;

    public int Index => this.m_Index;

    internal void Initialize(int index)
    {
        this.m_Index = index;
    }
}
