using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private AudioSource _audio;
    public Action<MusicBox> onOver;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Init(AudioClip clip)
    {
        if (clip == null) return;
        
        _audio.clip = clip;
        _audio.Play();
        StartCoroutine(Delay(clip.length));
    }

    private IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        onOver?.Invoke(this);
    }
}