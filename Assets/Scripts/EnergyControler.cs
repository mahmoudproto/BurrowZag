using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyControler : MonoBehaviour
{
    float current_energy;
    public int max_energy;
    public Slider EnergySlider;
    [Header("Bonus Percentages %")]
    [Space(5)]
    public int weakpoint_bonus;
    public int extraLife_bonus;
    public int maxCombo_bonus;

    [Header("Consume Energy Percentage %")]
    [Space(5)]
    public float Penetrate_cost;
    public float consumbtion_rate;
    //public Dictionary<EnergyBonusType, int> EnergyBonusVAlue;

    private void Start()
    {
        //EnergyBonusVAlue.Add(EnergyBonusType.weakpoint,5);
        EnergySlider.maxValue = max_energy;
        EnergySlider.value = max_energy;
        current_energy = max_energy;
        EventsTest.weakpoint_Hit_event += () => AddEnergy(weakpoint_bonus);
        InputController.onMouseUp += DecreaseEnergy;
        InputController.onMouseDown += () => CancelInvoke();
    }

    void AddEnergy(int bonus)
    {
        current_energy += (max_energy * bonus / 100);
        if (current_energy > max_energy)
            current_energy = max_energy;
    }

    void DecreaseEnergy()
    {
        if (current_energy < (Penetrate_cost * max_energy / 100))
        {
            Debug.Log("Not Enough Energy to Penetrate");
            return;
        }
        InvokeRepeating("DecreaseEnergyEverySecound", 0, consumbtion_rate);
    }
    void DecreaseEnergyEverySecound()
    {
        if (current_energy < (Penetrate_cost * max_energy / 100))
        {
            CancelInvoke();
            return;
        }
        current_energy -= (Penetrate_cost * max_energy / 100);
        EnergySlider.value = current_energy;
    }

}


public enum EnergyBonusType
{
    extra_life,
    weakpoint,
    MaxComboWP
}
