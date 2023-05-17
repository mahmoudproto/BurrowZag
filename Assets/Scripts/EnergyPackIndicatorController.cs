using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPackIndicatorController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject indicator;
    Vector3 newIndicatorPosition;
    Transform playerTransform;
    float verticalOffset;
    float indicatorScreenLimit;
    float indicatorXPosition;
    Vector2 collectiblePosition;

    void Start()
    {
        GameManager.OnNewEnergyPackGenerated += OnNewEnergyPackGeneratedHandler;
        playerTransform = GameManager.instance.playerTransform;
        verticalOffset = playerTransform.position.y - indicator.transform.position.y;
        indicatorScreenLimit = MathF.Abs(playerTransform.position.x - indicator.transform.position.x);
    }

    private void OnNewEnergyPackGeneratedHandler(Collectible collectible)
    {
        indicator.SetActive(true);
        collectiblePosition = collectible.transform.position;
        newIndicatorPosition = new Vector3(collectible.transform.position.x, indicator.transform.position.y);
        indicator.transform.position = newIndicatorPosition;
    }

    private void LateUpdate()
    {
        // if the indicator got past the collectible remove it and exit the update
        if( playerTransform.position.y - collectiblePosition.y <=  verticalOffset)
        {
            indicator.SetActive(false);
            return;
        }
        // limit the indicator rendering to the right of the screen  
        if ((playerTransform.position.x - collectiblePosition.x) <= indicatorScreenLimit * -1)
        {
            indicatorXPosition = playerTransform.position.x + indicatorScreenLimit;
        }
        // limit the indicator rendering to the left of the screen  
        else if ((playerTransform.position.x - collectiblePosition.x) >= indicatorScreenLimit)
        {
            indicatorXPosition = playerTransform.position.x - indicatorScreenLimit;
        }
        else
            indicatorXPosition = collectiblePosition.x;

        newIndicatorPosition = new Vector3(indicatorXPosition, playerTransform.position.y - verticalOffset);
        indicator.transform.position = newIndicatorPosition;
        arrow.transform.up = arrow.transform.position - (Vector3)collectiblePosition;
        //arrow.transform.RotateAround(indicator.transform.position, Vector3.forward, Vector3.Angle(collectiblePosition, indicator.transform.position));
    }

    private void OnDestroy()
    {
        GameManager.OnNewEnergyPackGenerated -= OnNewEnergyPackGeneratedHandler;
    }
}
