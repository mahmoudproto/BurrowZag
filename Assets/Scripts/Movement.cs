using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector2 movementDirection=Vector2.one;
    [SerializeField] int maxVerticalSpeed;
    [SerializeField] int maxHorizontalSpeed;
    [SerializeField] InputController inputController;
    [Range(0f,1f)]
    [SerializeField] float currentSpeed=.2f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    Rigidbody2D rigidbody2D;
    // Update is called once per frame
    void Update()
    {
        movementDirection.x = inputController.Direction*maxHorizontalSpeed;
        movementDirection.y = maxVerticalSpeed;

        rigidbody2D.velocity = movementDirection.normalized * currentSpeed ;
    }
}
