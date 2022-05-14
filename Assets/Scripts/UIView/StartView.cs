using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartView : UIBase
{
    private Animator _panelAnimator;
    private Button _startButton;
    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _panelAnimator = GetComponent<Animator>();
        _startButton = GetComponentInChildren<Button>();
        _startButton.onClick.AddListener(CloseThis);
    }

    private void CloseThis()
    {
        uiManager.CloseUI(index);
        uiManager.OpenUI(UIType.LevelView);
    }

    public override void Close()
    {
        _panelAnimator?.Play(UIManager.close);
        GameManager.Instance.OpenMoneyView();
    }
    public override void Open()
    {
        _panelAnimator?.Play(UIManager.open);
        GameManager.Instance.CloseMoneyView();
    }
}