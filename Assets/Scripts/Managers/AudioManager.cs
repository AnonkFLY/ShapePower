using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioClip[] soundEffect;
    [SerializeField]
    private AudioClip[] background;
    [SerializeField]
    private GameObject musicBox;
    private List<MusicBox> _over = new List<MusicBox>();
    private void Awake()
    {
        Instance = this;
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
        var i = _over.Count;
        if (i < 1)
        {
            return InstanceMusicBox();
        }
        var result = _over[i - 1];
        _over.RemoveAt(i - 1);
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
        _over.Add(musicBox);
    }
}
