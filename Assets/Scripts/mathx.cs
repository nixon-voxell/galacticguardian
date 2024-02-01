using Unity.Mathematics;
using Unity.Burst;

[BurstCompile]
public static class mathx
{
    [BurstCompile]
    public static int flatten_int2(in int2 x, int length)
    {
        return x.x + x.y * length;
    }
}

