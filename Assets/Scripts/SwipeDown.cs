using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SwipeDown : MonoBehaviour
{
    [SerializeField]
    private float minDeltaY;
    private float maxTime;

    private Vector2 onMouseDownPosition;
    private float onMouseDownTime;

    public event Action onDoubleTap;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            onMouseDownPosition = Input.mousePosition;
            onMouseDownTime = Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(0)
            && onMouseDownTime<maxTime
            && minDeltaY*minDeltaY>((Vector2)Input.mousePosition-onMouseDownPosition).sqrMagnitude)
        {
            onDoubleTap?.Invoke();
        }
    }
}
