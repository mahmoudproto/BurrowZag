using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiamondController : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleSystem;
    [SerializeField] SpriteRenderer SpriteRenderer;
    public static event Action onPlayerHitDiamond;
    
    void Initialize()
    {
        SpriteRenderer.enabled = true;
        ParticleSystem.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("character"))
        {
            onPlayerHitDiamond?.Invoke();
            SpriteRenderer.enabled=false;
            ParticleSystem.Play();
            Invoke("Initialize", 2);
        }
    }

    private void OnDestroy()
    {
        onPlayerHitDiamond = null;
    }
}
