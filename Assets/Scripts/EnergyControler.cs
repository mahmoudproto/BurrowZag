using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyControler : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField]float current_energy;
    public int max_energy;
    [SerializeField] Slider EnergySlider;
    [Header("Bonus Percentages %")]
    [Space(5)]
    public int weakpoint_bonus;
    public int extraLife_bonus;
    public int maxCombo_bonus;

    [Header("Consume Energy Percentage %")]
    [Space(5)]
    public float Penetrate_cost;
    public float consumbtion_rate;


    public ComboMultiplier comboMultiplier;
    public bool CanPenetrate { get => Penetrate_cost <= current_energy && BoostActive; }
    public static event Action<bool> onEnergysufficiencyChange;

    static EnergyControler instance;
    public static EnergyControler Instance => instance;
    public bool BoostActive { get; set; }
    private void Start()
    {
        instance = this;
        EnergySlider.maxValue = max_energy;
        EnergySlider.value = current_energy;
        InputController.onDoubleTap += DecreaseEnergy;
        InputController.onMouseDown += OnHoldHandler;
        GameManager.onGamePaused += OnGamePausedHandler;
        GameManager.onGameResumed += OnGameResumedHandler;
    }

    public bool Penetrate()
    {
        if (CanPenetrate)
        {
            current_energy -= Penetrate_cost;
            EnergySlider.value = current_energy;
            return true;
        }
        else
            return false;
    }

    public void AddEnergy(float energy)
    {
        current_energy += energy * comboMultiplier.CurrentStageMultiplier;

        EnergySlider.value = current_energy;
        if (current_energy > max_energy)
            current_energy = max_energy;
        if (current_energy >= Penetrate_cost)
            onEnergysufficiencyChange?.Invoke(true);
    }

    private void OnGamePausedHandler()
    {
        CancelInvoke();
        BoostActive = false;
    }

    private void OnGameResumedHandler()
    {
        if (!BoostActive)
            return;
        DecreaseEnergy();

    }

    private void OnHoldHandler()
    {
        CancelInvoke();
        BoostActive = false;
    }

    void DecreaseEnergy()
    {
        if (current_energy < (Penetrate_cost * max_energy / 100))
        {
            onEnergysufficiencyChange?.Invoke(false);
            return;
        }
        InvokeRepeating("DecreaseEnergyEverySecound", 0, consumbtion_rate);
        BoostActive = true;
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

    private void OnDestroy()
    {
        InputController.onDoubleTap -= DecreaseEnergy;
        InputController.onMouseDown -= OnHoldHandler;
        GameManager.onGamePaused -= OnGamePausedHandler;
    }

}