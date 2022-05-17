using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private AudioSource _audio;
    private WaitForSeconds wait = new WaitForSeconds(5);
    public Action<MusicBox> onOver;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    public void Init(AudioClip clip)
    {
        if (clip == null)
            return;
        _audio.clip = clip;
        _audio.Play();
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return wait;
        onOver?.Invoke(this);
    }
}
