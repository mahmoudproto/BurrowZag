using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // Mouse Events
    public UnityEngine.Events.UnityEvent<int> onDirectionChange;
    public UnityEngine.Events.UnityEvent onMouseDown;
    public UnityEngine.Events.UnityEvent onMouseUp;
    

    // direction and changing threshold
    [SerializeField] int direction = 0;
    public int Direction { private set { direction = value; } get { return direction; } }
    [SerializeField] float threshold = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Vector2 initPosition;
    Vector2 currentPosition;
    Vector2 delta;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            initPosition = Input.mousePosition;
            onMouseDown.Invoke();
        }
        if(Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            delta = currentPosition - initPosition;
            initPosition = currentPosition;
            if (delta.x > threshold)
            {
                direction = 1;
            }

            if (delta.x < -1 * threshold)
            {
                direction = -1;
            }

            onDirectionChange.Invoke(direction);
        }
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUp.Invoke();
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
