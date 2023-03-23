using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFPS : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 999;
    }
}
