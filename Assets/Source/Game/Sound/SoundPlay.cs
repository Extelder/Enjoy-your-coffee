using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _audioClipStart;
    [SerializeField] private AudioClip _audioClip;

    private void PlaySound()
    {
        _audio.clip = _audioClip;
        _audio.Play();
    }    
    private void PlaySoundStart()
    {
        _audio.clip = _audioClipStart;
        _audio.Play();
    }
    
}
