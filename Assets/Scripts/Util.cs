using System;
using System.Collections;
using UnityEngine;

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
}
