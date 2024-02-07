using UnityEngine;

public class GameStat : SingletonMono<GameStat>
{
    [SerializeField] private int m_StartingEssence;

    [Header("Display Only")]
    public int EssenceCount; // Current amt of essence
    public float Time;
    public int KeyCount;
    public int KillCount;
    public int EssenceTotalCollected; 
    public int EssenceSpent; // Not hooked up yet

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        this.EssenceCount = m_StartingEssence;
        this.Time = 0.0f;
        this.KeyCount = 0;
        this.KillCount = 0;
        this.EssenceTotalCollected = 0;
        this.EssenceSpent = 0;
    }

    public void AddEssence(int essenceAmt)
    {
        Debug.Log("[STATS] Essence Add: " + essenceAmt);

        EssenceCount += essenceAmt;
        if (essenceAmt > 0)
        {
            this.EssenceTotalCollected += essenceAmt;
        }
        else if (essenceAmt < 0)
        {
            this.EssenceSpent += Mathf.Abs(essenceAmt);
        }
    }

    public void AddKey()
    {
        KeyCount++;
    }

    private void Update()
    {
        this.Time += UnityEngine.Time.deltaTime;
    }
}
