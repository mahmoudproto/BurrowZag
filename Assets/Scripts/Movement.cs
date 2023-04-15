using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float initialSpeed;

    float currentSpeed;
    new Rigidbody2D rigidbody2D;
    Vector2 movementDirection=new Vector2(-1,-1);
    private int lastDiriction=1;
    private bool isEnergySufficent=true;

    void OnEnable()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        GameManager.instance.playerTransform = this.transform;
        currentSpeed = initialSpeed;

        InputController.onDirectionChange += OnDirictionChangeHandler;
        InputController.onMouseDown += OnMouseDownHandler;
        InputController.onMouseUp += OnMouseUpHandler;
        EnergyControler.onEnergysufficiencyChange += onInsufficantEnergyHandler;
    }

    void Update()
    {
        if (!GameManager.instance.GameisPaused)
            rigidbody2D.velocity = movementDirection.normalized * currentSpeed;
        else
            rigidbody2D.velocity = Vector2.zero;
    }
    void OnMouseDownHandler()
    {
        movementDirection.x = lastDiriction;
    }
    void OnDirictionChangeHandler(int diriction)
    {
        movementDirection.x = diriction;
        lastDiriction = diriction;
    }
    void OnMouseUpHandler()
    {
        //make the character move down on release 
        if (isEnergySufficent)
            movementDirection.x = 0;
        else
            movementDirection.x = lastDiriction;
    }
    private void onInsufficantEnergyHandler(bool isSufficent)
    {
        this.isEnergySufficent = isSufficent;
        if (isEnergySufficent == false)
            OnMouseDownHandler();
    }
    private void OnDestroy()
    {
        InputController.onDirectionChange -= OnDirictionChangeHandler;
        InputController.onMouseDown -= OnMouseDownHandler;
        InputController.onMouseUp -= OnMouseUpHandler;
        EnergyControler.onEnergysufficiencyChange -= onInsufficantEnergyHandler;
    }
}
