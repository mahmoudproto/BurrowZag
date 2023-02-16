using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector2 movementDirection=Vector2.one;
    [SerializeField] int maxVerticalSpeed;
    [SerializeField] int maxHorizontalSpeed;
    [SerializeField] InputController inputController;
    [SerializeField] float currentSpeed=.2f;

    new Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        InputController.onDirectionChange += OnDirictionChangeHandler;
        movementDirection.x = maxHorizontalSpeed;
        movementDirection.y = maxVerticalSpeed;
    }

    void OnDirictionChangeHandler(int diriction)
    {
        movementDirection.x = diriction * maxHorizontalSpeed;
    }

    void Update()
    {
        rigidbody2D.velocity = movementDirection.normalized * currentSpeed ;
    }
}
