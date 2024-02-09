using MilkShake;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShakerManager : SingletonMono<ShakerManager> 
{
    [SerializeField] private ShakePreset[] m_ShakePresetList;

    private Dictionary<string, ShakePreset> m_ShakePresetDict;
    private void Start()
    {
        this.m_ShakePresetDict = new Dictionary<string, ShakePreset>();
        for (int i = 0; i < m_ShakePresetList.Length; i++)
        {
            ShakePreset preset = this.m_ShakePresetList[i];
            this.m_ShakePresetDict.Add(preset.ShakerName, preset);
        }
        
    }

    public void Shake(string shakePresetName)
    {
        ShakePreset preset;
        if (this.m_ShakePresetDict.TryGetValue(shakePresetName, out preset))
        {
            Shaker.ShakeAll(preset);
        }
        else
        {
            Debug.LogWarning("Shake Preset not found: " + shakePresetName);
            return;
        }

    }

}
