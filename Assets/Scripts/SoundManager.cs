using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] bool separateSfxMusicSources;
    [Header("Game sounds")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip blockPenetrated;
    [SerializeField] AudioClip blockHit;
    [SerializeField] AudioClip collectable;
    [SerializeField] AudioClip boost;

    [Header("UI Sounds")]
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioClip menuPopup;

    [Header("Volume")]
    [SerializeField] [Range(0f,1f)]float sfxVolume;
    [SerializeField] [Range(0f,1f)]float musicVolume;
    [Header("Audio source, default")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource sfxAudioSource;
    private static SoundManager instance;
    public static SoundManager Instance => instance;

    public float SfxVolume {
        get => sfxVolume;
        set {
            sfxVolume = value;
            sfxAudioSource.volume = sfxVolume;
        } }
    public float MusicVolume {
        get => musicVolume;
        set
        {
            musicVolume = value;
            audioSource.volume = musicVolume;
        }
    }


    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance = this;
        if (separateSfxMusicSources)
        {
            sfxAudioSource = new GameObject("SfxAudioSource").AddComponent<AudioSource>();
            sfxAudioSource.playOnAwake = false;
            sfxAudioSource.volume = sfxVolume;
        }
        else
            sfxAudioSource = audioSource;
    }
    
    private void Start()
    {
        PlayBackground();

    }

    public void PlayBackground()
    {
        audioSource.clip = backgroundMusic;
        audioSource.volume = musicVolume;
        audioSource.Play();
    }
    public void PlayBlockPenetrated()
    {
        sfxAudioSource.PlayOneShot(blockPenetrated, sfxVolume);
    }
    public void PlayBlockHit()
    {
        sfxAudioSource.PlayOneShot(blockHit, sfxVolume);
    }
    public void PlayCollectible()
    {
        sfxAudioSource.PlayOneShot(collectable, sfxVolume);
    }
    public void PlayBoost()
    {
        sfxAudioSource.PlayOneShot(boost, sfxVolume);
    }

}
