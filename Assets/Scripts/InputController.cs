using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    // Mouse Events
    public static event Action<int> onDirectionChange;
    public static event Action onMouseDown;
    public static event Action onMouseUp;
    public static event Action onDoubleTap;

    // direction and changing threshold
    [SerializeField] int direction;
    public int Direction { private set { direction = value; } get { return direction; } }
    [SerializeField] float directionChangeThreshold = 0.5f;

    [SerializeField] float doubleTapSpeed;

    Vector2 initPosition;
    Vector2 currentPosition;
    Vector2 delta;
    void Start()
    {
        DoubleTapDetector.Instance.onDoubleTap += onDoubleTap;
    }
    private void OnDestroy()
    {
        DoubleTapDetector.Instance.onDoubleTap -= onDoubleTap;
    }


    void Update()
    {
        if (GameManager.instance.GameisPaused)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            initPosition = Input.mousePosition;
            onMouseDown?.Invoke();
        }
        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            delta = currentPosition - initPosition;
            initPosition = currentPosition;
            if (delta.x > directionChangeThreshold)
            {
                if (direction != 1)
                {
                    direction = 1;
                    onDirectionChange?.Invoke(direction);
                }
            }

            if (delta.x < -1 * directionChangeThreshold)
            {
                if (direction!=-1)
                {
                    direction = -1;
                    onDirectionChange?.Invoke(direction);
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUp?.Invoke();
        }


        //print(Input.touchCount);

        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).deltaPosition.x > threshold)
        //    {
        //        direction = 1;
        //    }

        //    if (Input.GetTouch(0).deltaPosition.x < -1 * threshold)
        //    {
        //        direction = -1;
        //    }
        //}
    }
}
