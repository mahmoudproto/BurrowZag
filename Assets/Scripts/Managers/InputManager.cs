using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    // Mouse Events
    public static event Action<int> onDirectionChange;
    public static event Action onMouseDown;
    public static event Action onMouseUp;
    public static event Action onStartDive;
    public static string controlScheme="double tap";

    // direction and changing threshold
    [SerializeField] int direction;
    public int Direction { private set { direction = value; } get { return direction; } }
    [Tooltip("Sensitivity")]
    [SerializeField] float directionChangeThreshold = 0.5f;

    [SerializeField] float doubleTapCoolDown;
    Vector2 initPosition;
    Vector2 currentPosition;
    Vector2 delta;
    float lastMouseDown;
    const float maxDownDuration=0.2f;
    bool isDiving;

    void Update()
    {
        if (GameManager.instance.GameisPaused)
            return;

        //double tap detection
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastMouseDown <= maxDownDuration && isDiving == false&&controlScheme=="double tap")
            {
                direction = 0;
                onStartDive?.Invoke();
                isDiving = true;
                Invoke("EnableDoubleTap", doubleTapCoolDown);
            }
            else
            {
                onMouseDown?.Invoke();
            }

            lastMouseDown = Time.time;
            initPosition = Input.mousePosition;
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

        if (Input.GetMouseButtonUp(0)&&controlScheme=="release finger")
        {
            onStartDive?.Invoke();
        }
    }

    private void EnableDoubleTap()
    {
        isDiving = false;
    }

    private void OnDestroy()
    {
        onStartDive = null;
        onMouseDown = null;
        onMouseUp = null;
        onDirectionChange = null;
    }
}
