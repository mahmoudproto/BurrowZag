using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFPS : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Target = 60 fps");
        Application.targetFrameRate = 999;
    }
}
