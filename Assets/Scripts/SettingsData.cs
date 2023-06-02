using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    static float audioLevel;
    static float sfxLevel;

    public static float AudioLevel
    {
        get => audioLevel;
        set
        {
            audioLevel = value;
            PlayerPrefs.SetFloat("audioLevel", audioLevel);
        }
    }
    public static float SfxLevel
    {
        get => sfxLevel;
        set
        {
            sfxLevel = value;
            PlayerPrefs.SetFloat("sfxLevel", sfxLevel);
        }
    }
    public static void Load()
    {
        audioLevel = PlayerPrefs.GetFloat("audioLevel");
        sfxLevel = PlayerPrefs.GetFloat("sfxLevel");

    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("audioLevel",audioLevel);
        PlayerPrefs.SetFloat("sfxLevel", sfxLevel);

    }
}
