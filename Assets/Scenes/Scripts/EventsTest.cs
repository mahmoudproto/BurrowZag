using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsTest : MonoBehaviour
{
    public delegate void weakpoint_Hit();
    public static event weakpoint_Hit weakpoint_Hit_event;

    public delegate void release();
    public static event release onRelease_event;

    public delegate void Hold();
    public static event Hold onHold_event;


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
        if (onHold_event != null)
            onHold_event.Invoke();
    }

}
