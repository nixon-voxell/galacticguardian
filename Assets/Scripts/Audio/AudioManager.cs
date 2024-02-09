using System;
using UnityEngine;

/* if want to call any music here in other script, 
 * call those functions at region Public Call Functions
 * 
 * example: AudioManager.Instance.PlaySfx("Place Bomb");
 * example: AudioManager.Instance.BgmSource.Stop();
*/
public class AudioManager : SingletonMono<AudioManager>
{
    [Header("Sound")]
    [SerializeField] private SoundElement[] m_BgmSoundList, m_SfxSoundList;
    public AudioSource BgmSource, SfxSource;

    #region Public Call Functions
    public void PlayBgm(string musicName)
    {
        SoundElement ele = GetMusic(m_BgmSoundList, musicName);
        PlayBgm(ele, BgmSource);
    }

    public void PlaySfx(string musicName)
    {
        SoundElement ele = GetMusic(m_SfxSoundList, musicName);
        PlaySfx(ele, SfxSource);
    }

    #region Only UI
    public void ToggleMute(AudioSource source)  //AudioManager.Instance.BgmSource
    {
        source.mute = !source.mute;
    }

    #endregion
    #endregion

    #region Private Quick Functions
    private SoundElement GetMusic(SoundElement[] list, string musicName)
    {
        SoundElement ele = Array.Find(list, x => x.SoundName == musicName);

        return ele;
    }

    private void PlayBgm(SoundElement ele, AudioSource source)
    {
        if (!ele.Equals(default(SoundElement)))
        {
            source.clip = ele.Clip;
            source.Play();
        }
        else { return; }
    }

    private void PlaySfx(SoundElement ele, AudioSource source)
    {
        if (!ele.Equals(default(SoundElement)))
        {
            source.clip = ele.Clip;
            source.PlayOneShot(ele.Clip);
        }
        else { return; }
    }
    #endregion
}
