using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class DoubleTapDetector : MonoBehaviour
{
    float lastTap = 0;
    //float currentTap = 0;
    float lastMouseDown = 0;
    float lastMouseUp = 0;

    [SerializeField] float maxDownDuration=.5f;
    [SerializeField] float maxDurationBetweenTaps=.5f;

    //float downDuration;
    //float durationBetweenTaps;

    [SerializeField] float maxDownUpDistance = 1f;
    Vector2 downPosition;
    Vector2 positionDelta;
    [SerializeField] float doubleTapCoolDown=2;
    static DoubleTapDetector instance;

    public static DoubleTapDetector Instance => instance;// ? instance : instance = new GameObject("DoubleTap").AddComponent<DoubleTapDetector>();

    public event Action onDoubleTap;

    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            //DownDuration = 0;
            downPosition = Input.mousePosition;
            lastMouseDown = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            positionDelta = (Vector2)Input.mousePosition - downPosition;
            if (Time.time - lastMouseDown < maxDownDuration)
            {
                if (Time.time - lastTap < maxDurationBetweenTaps && 
                    positionDelta.sqrMagnitude<(maxDownUpDistance*maxDownUpDistance))
                {
                    //print("DOuble tap");
                    onDoubleTap?.Invoke();
                    lastTap = 0;
                    lastMouseUp = 0;
                    lastMouseDown = 0;
                    Disable();
                    Invoke("Enable",doubleTapCoolDown);
                }
                else
                    lastTap = Time.time;
            }
            else
                lastMouseUp = Time.time;

            #region oldCode
            //print("Time: " + Time.time);
            //if (Input.GetMouseButton(0))
            //{
            //    downDuration += Time.deltaTime;
            //}
            //else
            //{
            //    durationBetweenTaps += Time.deltaTime;
            //}
            //if (downDuration < maxDownDuration)
            //{
            //    if (durationBetweenTaps < maxDurationBetweenTaps)
            //    {
            //        print("Double Click");
            //        onDoubleClick?.Invoke();
            //    }
            //    durationBetweenTaps = 0;
            //    lastTap = Time.time;
            //}
            #endregion
        }
    }
    private void Enable()
    {
        enabled = true;
    }

    private void Disable()
    {
        enabled = false;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        //DontDestroyOnLoad(gameObject);
    }
}
