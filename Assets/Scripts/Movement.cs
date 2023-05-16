using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float initialSpeed = 6;
    [SerializeField] float energizedSpeed = 12;

    [SerializeField] AnimationCurve boostSpeedTransition;
    [SerializeField] float boostTransitionDuration;

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
        normalSpeed = initialSpeed;
        currentSpeed = normalSpeed;
        InputController.onDirectionChange += OnDirictionChangeHandler;
        InputController.onMouseDown += OnMouseDownHandler;
        //InputController.onMouseUp += OnMouseUpHandler;
        EnergyControler.onEnergysufficiencyChange += onInsufficantEnergyHandler;
        GameManager.onStageChangeEvent += OnStageChangeHandler;
        InputController.onDoubleTap += OnMouseUpHandler;
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
        currentSpeed = normalSpeed;
        //CancelInvoke("SlowMo");
        CancelInvoke("SpeedTransition");
        movementDirection.x = lastDiriction;
        EnergyControler.Instance.BoostActive = false;
    }
    void OnDirictionChangeHandler(int diriction)
    {
        if (movementDirection.x == diriction)
            return;
        movementDirection.x = diriction;
        this.transform.Rotate(Vector3.forward, diriction * 90);
        lastDiriction = diriction;
    }
    void OnMouseUpHandler()
    {
        //make the character move down on release 
        if (isEnergySufficent)
        {
            SoundManager.Instance.PlayBoost();
            //normalSpeed = currentSpeed;
            movementDirection.x = 0;
            //StartCoroutine(SlowMo(initialSpeed / 4f, energizedSpeed, .2f));

            StartCoroutine(SpeedTransition(normalSpeed, energizedSpeed));
            EnergyControler.Instance.BoostActive = true ;
        }
        else
        {
            //currentSpeed = normalSpeed;
            movementDirection.x = lastDiriction;
        }
    }
    
    private void onInsufficantEnergyHandler(bool isSufficent)
    {
        this.isEnergySufficent = isSufficent;
        if (isEnergySufficent == false)
            OnMouseDownHandler();
    }

    private void OnStageChangeHandler(StageInformations stage)
    {
        normalSpeed = initialSpeed * stage.speed_multiplier;
    }

    private void OnDestroy()
    {
        InputController.onDirectionChange -= OnDirictionChangeHandler;
        InputController.onMouseDown -= OnMouseDownHandler;
        InputController.onDoubleTap -= OnMouseUpHandler;
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

    IEnumerator SpeedTransition(float startSpeed, float targetSpeed)
    {
        for(float i=0;i<boostTransitionDuration;)
        {
            currentSpeed = startSpeed + boostSpeedTransition.Evaluate(i / boostTransitionDuration)*targetSpeed;
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
