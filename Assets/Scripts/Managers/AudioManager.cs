using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance { get => instance; }
    [SerializeField]
    private AudioClip[] soundEffect;
    [SerializeField]
    private AudioClip[] background;
    [SerializeField]
    private GameObject musicBox;
    private List<MusicBox> over = new List<MusicBox>();
    private void Awake()
    {
        instance = this;
    }
    public void PlaySoundEffect(int i)
    {
        GetMusicBox().Init(soundEffect[i]);
    }
    public void ChangeBackground(int i)
    {

    }
    private MusicBox GetMusicBox()
    {
        var i = over.Count;
        if (i < 1)
        {
            return InstanceMusicBox();
        }
        var result = over[i - 1];
        over.RemoveAt(i - 1);
        return result;
    }
    private MusicBox InstanceMusicBox()
    {
        var result = Instantiate(musicBox, transform).GetComponent<MusicBox>();
        result.onOver += OnOverEvent;
        return result;
    }
    private void OnOverEvent(MusicBox musicBox)
    {
        over.Add(musicBox);
    }
}
