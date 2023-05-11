using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float energyToAdd;
    [SerializeField] string characterTag;
    public float EnergyToAdd 
    { 
        get=>energyToAdd; 
        set=>energyToAdd=value;
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
            EnergyControler.Instance.AddEnergy(energyToAdd);
            onCollictibleHit?.Invoke(energyToAdd);
        }
    }
}
