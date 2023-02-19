using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public static event Action<bool> onEnergysufficiencyChange;
    private void Start()
    {
        EnergySlider.maxValue = max_energy;
        EnergySlider.value = max_energy;
        current_energy = max_energy;
        EventsTest.weakpoint_Hit_event += () => AddEnergy(weakpoint_bonus);
        InputController.onMouseUp += DecreaseEnergy;
        InputController.onMouseDown += () => CancelInvoke();
    }

    private float deltaTime;
    [SerializeField] TMPro.TMP_Text FPS_text;
    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        FPS_text.text = "FPS : " + Mathf.Ceil(fps).ToString();
    }

    void AddEnergy(int bonus)
    {
        current_energy += (max_energy * bonus / 100);
        EnergySlider.value = current_energy;
        if (current_energy > max_energy)
            current_energy = max_energy;
        if (current_energy >= Penetrate_cost)
            onEnergysufficiencyChange?.Invoke(true);
    }

    void DecreaseEnergy()
    {
        if (current_energy < (Penetrate_cost * max_energy / 100))
        {
            onEnergysufficiencyChange?.Invoke(false);
            return;
        }
        InvokeRepeating("DecreaseEnergyEverySecound", 0, consumbtion_rate);
    }
    void DecreaseEnergyEverySecound()
    {
        if (current_energy < (Penetrate_cost * max_energy / 100))
        {
            onEnergysufficiencyChange?.Invoke(false);
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
