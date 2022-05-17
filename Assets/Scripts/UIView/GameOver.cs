using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameOver : UIBase
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public override void Open()
    {
        _animator.Play(UIManager.open);
    }
    public override void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public void BackLevel()
    {
        if (type == UIType.GameWin)
        {
            GameManager.Instance.UnLockNextLevel();
            DebugLog.Message("解锁下一关");
        }
        DebugLog.Message("返回主菜单");
        StartCoroutine(GameManager.Instance.BackLevel());
    }

}