using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Burst;

public class Util
{
    public static void CallFunctionNextFrame(MonoBehaviour monoBehaviour, Action func)
    {
        monoBehaviour.StartCoroutine(_CallFunctionNextFrame(func));
    }

    public static void CallFunctionNextSeconds(MonoBehaviour monoBehaviour, float seconds, Action func)
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

    [BurstCompile]
    public static void CalculateTimeFromSeconds(
        in int inSeconds,
        out int hours,
        out int minutes,
        out int seconds
    )
    {
        const int HR_SECONDS = 60 * 60;
        const int MIN_SECONDS = 60;

        int secondsLeft = inSeconds;

        hours = secondsLeft / HR_SECONDS;
        secondsLeft -= hours * HR_SECONDS;

        minutes = secondsLeft / MIN_SECONDS;
        secondsLeft -= minutes * MIN_SECONDS;

        seconds = secondsLeft;
    }

    public static void SetBorderColor(VisualElement element, Color color)
    {
        element.style.borderTopColor = color;
        element.style.borderLeftColor = color;
        element.style.borderRightColor = color;
        element.style.borderBottomColor = color;
    }
}
