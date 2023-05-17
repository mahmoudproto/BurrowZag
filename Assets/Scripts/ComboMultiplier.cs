using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ComboMultiplier 
{
    [SerializeField] float[] multiplierStages = { 1, 5, 10 };   
    [SerializeField] float[] audioPitchPerStage = { 1, 1, 1 };
    int stageIndex=0;
    public float CurrentStageMultiplier
    {
        get
        {
            return multiplierStages[stageIndex];
        }
    }
    public float CurrentStagePitch
    {
        get
        {
            return audioPitchPerStage[stageIndex];
        }
    }

    public void IncreaseStage()
    {
        stageIndex = Mathf.Min(stageIndex + 1, multiplierStages.Length - 1);
    }


}
