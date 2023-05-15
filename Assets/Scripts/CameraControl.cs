using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 offset;
    void Start()
    {
        offset =this.transform.position- GameManager.instance.playerTransform.position;
    }

    private void LateUpdate()
    {
        this.transform.position = GameManager.instance.playerTransform.position + offset;   
    }
}
