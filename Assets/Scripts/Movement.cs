using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float initialSpeed = 6;
    [SerializeField] float energizedSpeed = 12;

    float currentSpeed;
    new Rigidbody2D rigidbody2D;
    Vector2 movementDirection=new Vector2(-1,-1);
    private int lastDiriction=1;
    private bool isEnergySufficent=true;
    float normalSpeed;
    void OnEnable()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        GameManager.instance.playerTransform = this.transform;
        currentSpeed = initialSpeed;
        normalSpeed = currentSpeed;
        InputController.onDirectionChange += OnDirictionChangeHandler;
        InputController.onMouseDown += ReturnToNormalSpeed;
        InputController.onMouseUp += OnMouseUpHandler;
        EnergyControler.onEnergysufficiencyChange += onInsufficantEnergyHandler;
        GameManager.onStageChangeEvent += OnStageChangeHandler;
    }

    void Update()
    {
        if (!GameManager.instance.GameisPaused)
            rigidbody2D.velocity = movementDirection.normalized * currentSpeed;
        else
            rigidbody2D.velocity = Vector2.zero;
    }

    void ReturnToNormalSpeed()
    {
        currentSpeed = normalSpeed;
        CancelInvoke("SlowMo");
        movementDirection.x = lastDiriction;
        EnergyControler.Instance.BoostActive = false;
    }
    void OnDirictionChangeHandler(int diriction)
    {
        movementDirection.x = diriction;
        this.transform.Rotate(Vector3.forward, diriction * 90);
        lastDiriction = diriction;
        ReturnToNormalSpeed();
    }
    void OnMouseUpHandler()
    {
        if (movementDirection.x == 0)
            return;
        normalSpeed = currentSpeed;
        Debug.Log("OnMouseUpHandler called");
        //make the character move down on release 
        if (isEnergySufficent)
        {
            SoundManager.Instance.PlayBoost();
            movementDirection.x = 0;
            StartCoroutine(SlowMo(initialSpeed / 4f, energizedSpeed, .2f));
            EnergyControler.Instance.BoostActive = true ;
        }
        else
        {
            movementDirection.x = lastDiriction;
        }

    }
    
    private void onInsufficantEnergyHandler(bool isSufficent)
    {
        this.isEnergySufficent = isSufficent;
        if (isEnergySufficent == false)
            ReturnToNormalSpeed();
    }

    private void OnStageChangeHandler(StageInformations stage)
    {
        currentSpeed = initialSpeed * stage.speed_multiplier;
    }

    private void OnDestroy()
    {
        InputController.onDirectionChange -= OnDirictionChangeHandler;
        InputController.onMouseDown -= ReturnToNormalSpeed;
        InputController.onMouseUp -= OnMouseUpHandler;
        EnergyControler.onEnergysufficiencyChange -= onInsufficantEnergyHandler;
    }

    IEnumerator SlowMo(float startSpeed,  float targetSpeed, float step)
    {
        for (float i = startSpeed, astep=step; i <= targetSpeed; astep=astep+astep, i += astep)
        {
            currentSpeed = Mathf.Min(energizedSpeed, i+astep);
            yield return new WaitForSeconds(0.1f);
        }

    }

}
