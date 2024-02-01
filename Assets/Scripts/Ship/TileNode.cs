using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileNode : MonoBehaviour
{
    public List<TileNode> Neighbors;
    public bool Connected = false;
    public Button BuildBtn;

    private bool m_Active = false;
    public bool Active => this.m_Active;
    public bool m_CanBuild = false;
    public bool CanBuild => this.m_CanBuild;

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
        this.m_Active = active;
    }

    public void SetCanBuild(bool canBuild)
    {
        this.m_CanBuild = canBuild;
        this.BuildBtn.visible = canBuild;
    }

    public void Reset()
    {
        this.Connected = false;
        this.m_Active = false;
    }
}
