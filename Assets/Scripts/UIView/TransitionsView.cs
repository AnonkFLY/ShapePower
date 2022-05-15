using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsView : UIBase
{
    private Animator _animator;
    public Action onCompleted;
    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _animator = GetComponent<Animator>();
        var trans = GetComponentsInChildren<RectTransform>();
        var height = Screen.height / 4;
        for (int i = 1; i < trans.Length; i++)
        {
            trans[i].sizeDelta = new Vector2(0, height);
            trans[i].localPosition = Vector3.down * ((i - 1) * height);
        }
    }
    public override void Close()
    {
        //_animator.Play(UIManager.close);
        _animator.SetBool("Open", false);
    }
    public override void Open()
    {
        _animator.SetBool("Open", true);
        _animator.Play(UIManager.open);
    }
    public void Completed()
    {
        onCompleted?.Invoke();
    }
}
