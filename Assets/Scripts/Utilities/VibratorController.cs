using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;
public class VibratorController : MonoBehaviour
{

    static VibratorController instance;
    public static VibratorController Instance {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Vibrator Controller").AddComponent<VibratorController>();
                return instance;
            }
            else
                return instance;
        }

    }
    /// <summary>
    /// Vibrates
    /// </summary>
    public void Vibrate()
    {
        //Handheld.Vibrate();
        Vibration.Vibrate(100,250);
    }
    /// <summary>
    /// Vibrates for secs seconds.
    /// Not implemented yet!
    /// </summary>
    /// <param name="secs"></param>
    public void Vibrate(float secs, int amplitude=-1)
    {
        Vibration.Vibrate((long)(secs * 1000), amplitude);
    }

    public void Vibrate(long[] pattern, int repeat=-1)
    {

        Vibration.Vibrate(pattern,repeat: repeat);
    }
}
