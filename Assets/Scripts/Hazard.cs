using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onShatter;
    public UnityEngine.Events.UnityEvent onInitialize;

    public void Initialize()
    {
        onInitialize?.Invoke();
    }

    public bool Shatter()
    {
        bool penetrated = EnergyControler.Instance ? EnergyControler.Instance.Penetrate() : false;
        if(penetrated)
        {
            onShatter?.Invoke();
            SoundManager.Instance.PlayBlockPenetrated();
            Invoke("Initialize", 3);
        }
        return penetrated;
    }

    private void OnEnable()
    {
        Initialize();
    }
}
