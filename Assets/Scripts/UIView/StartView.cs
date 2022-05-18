using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartView : UIBase, IUpdatable
{
    private Animator _panelAnimator;
    private Button _startButton;
    private TMP_Text _buttonText;
    private bool isQuit;
    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _panelAnimator = GetComponent<Animator>();
        _startButton = GetComponentInChildren<Button>();
        _buttonText = _startButton.GetComponentInChildren<TMP_Text>();
        _startButton.onClick.AddListener(CloseThis);
    }

    private void CloseThis()
    {
        AudioManager.Instance.PlaySoundEffect(8);
        uiManager.OpenUI(UIType.LevelView, true);
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

    public void OnUpdateView()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        isQuit = !isQuit;
        if (isQuit)
        {
            _buttonText.text = "<color=red>Quit</color>";
            _startButton.onClick.AddListener(Application.Quit);
            _startButton.onClick.RemoveListener(CloseThis);
            return;
        }
        _buttonText.text = "Start";
        _startButton.onClick.AddListener(CloseThis);
        _startButton.onClick.RemoveListener(Application.Quit);
    }
}