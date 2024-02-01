using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Util
{
    public static void CallFunctionNextFrame(MonoBehaviour monoBehaviour, Action func)
    {
        monoBehaviour.StartCoroutine(_CallFunctionNextFrame(func));
    }

    public static void CallFunctionNextSeconds(MonoBehaviour monoBehaviour, Action func, float seconds)
    {
        monoBehaviour.StartCoroutine(_CallFunctionNextFrame(func, seconds));
    }

    private static IEnumerator _CallFunctionNextFrame(Action func, float seconds = 0)
    {

        if (seconds > 0)
        {
            yield return new WaitForSeconds(seconds);
        }
        else
            yield return null;

        func();
    }

    public static Quaternion LookAt2DRotation(Vector2 objectPos, Vector2 targetPos)
    {
        Vector3 dir = targetPos - objectPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
