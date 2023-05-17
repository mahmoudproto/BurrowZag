using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ComboMultiplier 
{
    [SerializeField] float[] multiplierStages = { 1, 5, 10};
    int stageIndex=0;
    public float CurrentStageMultiplier {
        get {
            stageIndex = Mathf.Min(stageIndex + 1, multiplierStages.Length - 1);
            return multiplierStages[stageIndex];
        } 
    }


}
