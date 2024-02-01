public class GameStat : SingletonMono<GameStat>
{
    public int EssenceCount;
    public float Time;

    public void Reset()
    {
        this.EssenceCount = 0;
        this.Time = 0.0f;
    }
}
