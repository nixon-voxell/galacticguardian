using UnityEngine;

[System.Serializable]
public class Pool<T> : System.IDisposable
where T : Component
{
    public int Count;

    [SerializeField] private T m_Prefab;
    [SerializeField] private bool m_DefaultActive;

    private Transform m_Parent;
    private T[] m_Objects;
    private int m_CurrIdx;

    public T[] Objects => this.m_Objects;

    public void Initialize(Transform parent)
    {
        this.m_Parent = parent;
        _Initialize();
    }

    public void Initialize(Transform parent, T prefab, int pfxCount, bool defaultActive)
    {
        this.m_Parent = parent;
        this.m_Prefab = prefab;
        this.Count = pfxCount;
        this.m_DefaultActive = defaultActive;

        _Initialize();
    }

    public void SetAllObjectActive(bool active)
    {
        for (int o = 0; o < this.Count; o++)
        {
            this.m_Objects[o].gameObject.SetActive(active);
        }
    }

    public T GetNextObject()
    {
        T nextObj = this.m_Objects[this.m_CurrIdx];
        this.m_CurrIdx = this.GetNextIdx();
        return nextObj;
    }

    private int GetNextIdx()
    {
        return (this.m_CurrIdx + 1) % this.Count;
    }

    public void Dispose()
    {
#if UNITY_EDITOR
        if (this.m_Prefab == null)
        {
            Debug.LogWarning("Pool prefab not assigned, unable to dispose.", this.m_Parent);
            return;
        }
#endif
        for (int o = 0; o < this.Count; o++)
        {
            Object.Destroy(this.m_Objects[o]);
        }

        this.m_CurrIdx = 0;
    }

    private void _Initialize()
    {
#if UNITY_EDITOR
        if (this.m_Prefab == null)
        {
            Debug.LogWarning("Pool prefab not assigned, unable to initialize.", m_Parent);
            return;
        }

        if (this.Count <= 0)
        {
            Debug.LogWarning("Pool count cannot be less than or equal to 0, unable to initialize.", m_Parent);
            return;
        }
#endif
        this.m_Objects = new T[this.Count];
        this.m_CurrIdx = 0;

        for (int o = 0; o < this.Count; o++)
        {
            T obj = Object.Instantiate<T>(this.m_Prefab, this.m_Parent);
            obj.gameObject.SetActive(this.m_DefaultActive);

            // assign a pool index
            obj.gameObject.AddComponent<PoolIndex>();
            PoolIndex poolIndex = obj.GetComponent<PoolIndex>();
            poolIndex.Initialize(o);
             
            this.m_Objects[o] = obj;
        }
    }

}
