using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeSettings : MonoBehaviour
{
    [field :SerializeField] public AudioMixer Mixer { get; private set; }
    [field :SerializeField] public string MixerKey { get; private set; }
    [field :SerializeField] public Slider Slider { get; private set; }
    
    public PlayerConfig Config { get; private set; }

    private void Start()
    {
        Config = PlayerConfig.Instance;
        StartVirtual();
    }

    public virtual void StartVirtual()
    { }

    public virtual void ChangeSoundVolume(float value)
    {
        if (value == 0)
        {
            Mixer.SetFloat( MixerKey+ "Volume", -80);
        }
        else
        {
            Mixer.SetFloat(MixerKey + "Volume", Mathf.Log10(value) * 20);
        }
    }
}
