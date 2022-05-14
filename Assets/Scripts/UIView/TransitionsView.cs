using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsView : UIBase
{
    private Animator _animator;
    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _animator = GetComponent<Animator>();
    }
    public override void Close()
    {
        base.Close();
        _animator.Play(UIManager.close);
    }
    public override void Open()
    {
        base.Open();
        _animator.Play(UIManager.open);
    }
}
