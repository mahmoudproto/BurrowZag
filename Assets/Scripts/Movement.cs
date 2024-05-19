using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float initialSpeed = 6;
    public float InitialSpeed { get => initialSpeed;}
    [SerializeField] float energizedSpeed = 12;

    [SerializeField] AnimationCurve boostSpeedTransition;
    [SerializeField] float boostTransitionDuration;

    float currentSpeed;
    public float CurrentSpeed { get => currentSpeed; }
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
        InputManager.onDirectionChange += ChangeDirection;
        InputManager.onMouseDown += DeactivateEnergyBoost;
        //InputController.onMouseUp += OnMouseUpHandler;
        EnergyControler.onEnergysufficiencyChange += onInsufficantEnergyHandler;
        GameManager.onStageChangeEvent += OnStageChangeHandler;
        InputManager.onStartDive += ActivateEnergyBoost;
    }

    void Update()
    {
        if (!GameManager.instance.GameisPaused)
            rigidbody2D.velocity = movementDirection.normalized * currentSpeed;
        else
            rigidbody2D.velocity = Vector2.zero;
    }

    void DeactivateEnergyBoost()
    {
        currentSpeed = normalSpeed;
        //CancelInvoke("SlowMo");
        StopCoroutine("SpeedTransition");
        movementDirection.x = lastDiriction;
        EnergyControler.Instance.BoostActive = false;
    }
    void ChangeDirection(int direction)
    {
        if (movementDirection.x == direction || movementDirection.x == 0)
            return;
        movementDirection.x = direction;
        this.transform.Rotate(Vector3.forward, direction * 90);
        lastDiriction = direction;
    }
    void ActivateEnergyBoost()
    {
        //make the character move down on release 
        if (isEnergySufficent)
        {
            SoundManager.Instance.PlayBoost();
            //normalSpeed = currentSpeed;
            movementDirection.x = 0;
            //StartCoroutine(SlowMo(initialSpeed / 4f, energizedSpeed, .2f));

            EnergyControler.Instance.BoostActive = true ;
            StartCoroutine(SpeedTransition(normalSpeed, energizedSpeed));
        }
        else
        {
            //currentSpeed = normalSpeed;
            //movementDirection.x = lastDiriction;
        }
    }
    
    private void onInsufficantEnergyHandler(bool isSufficent)
    {
        Debug.Log("energy sufficiency changed to " +isSufficent);
        this.isEnergySufficent = isSufficent;
        if (isEnergySufficent == false)
            DeactivateEnergyBoost();
    }

    private void OnStageChangeHandler(StageInformations stage)
    {
        normalSpeed = initialSpeed * stage.speed_multiplier;
        currentSpeed = normalSpeed;
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
            if (!EnergyControler.Instance.BoostActive)
                break;
            currentSpeed = startSpeed + boostSpeedTransition.Evaluate(i / boostTransitionDuration) * targetSpeed;
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
