using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsTest : MonoBehaviour
{
    public static event Action weakpoint_Hit_event;
    public static event Action onRelease_event;
    public static event Action onHold_event;


    [ContextMenu("Weakpoint_Hit_event")]
    public void OnWeakpoint_Hit_event()
    {
        if (weakpoint_Hit_event != null)
            weakpoint_Hit_event.Invoke();
    }

    [ContextMenu("Release_event")]
    public void OnRelease_event()
    {
        if (onRelease_event != null)
            onRelease_event.Invoke();
    }

    [ContextMenu("Hold_event")]
    public void OnHold_event()
    {
        onHold_event?.Invoke();
    }

}
