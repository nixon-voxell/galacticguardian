using MilkShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milkshake_Test : MonoBehaviour
{
    public ShakePreset ShakePreset1;
    public ShakePreset ShakePreset2;
    public ShakePreset ShakePreset3;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Shaker.ShakeAll(ShakePreset1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Shaker.ShakeAll(ShakePreset2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Shaker.ShakeAll(ShakePreset3);
        }
    }
}
