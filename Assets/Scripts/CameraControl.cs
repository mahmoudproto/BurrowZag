using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;
    void Start()
    {
        offset =this.transform.position-player.position;
    }

    private void LateUpdate()
    {
        this.transform.position = player.position + offset;   
    }
}
