using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onShatter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Shatter()
    {
        bool penetrated = EnergyControler.Instance ? EnergyControler.Instance.Penetrate() : false;
        if(penetrated)
        {
            onShatter?.Invoke();
        }
        return penetrated;
    }
}
