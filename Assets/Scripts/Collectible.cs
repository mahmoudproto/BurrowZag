using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private float energyToAdd;
    [SerializeField] string characterTag;

    static int lastCollectibleIndex=0;
    public int MyIndex { get; set; }
    public float EnergyToAdd 
    {
        get => energyToAdd;
        set => energyToAdd = value;
    }

    private void Start()
    {
        onCollictibleHit.AddListener(GameManager.instance.Fire_EnergyPack_Hit_event);
    }
    public UnityEngine.Events.UnityEvent<float> onCollictibleHit;
    // Start is called before the first frame update
    void OnHit()
    {
        onCollictibleHit?.Invoke(energyToAdd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == characterTag)
        {
            if (lastCollectibleIndex + 1 == MyIndex)
                EnergyControler.Instance.comboMultiplier.IncreaseStage();
            else
                EnergyControler.Instance.comboMultiplier.ResetStage();
            EnergyControler.Instance.AddEnergy(energyToAdd);
            SoundManager.Instance.PlayCollectible();
            onCollictibleHit?.Invoke(energyToAdd);
        }
    }
}
