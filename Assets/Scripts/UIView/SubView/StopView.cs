using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StopView : MonoBehaviour
{
    private Button[] _buttons;
    private CanvasGroup _canvsGourp;
    public Action onStop;
    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
        _buttons[0].onClick.AddListener(Close);
        _buttons[1].onClick.AddListener(BackLevel);
        _canvsGourp = GetComponent<CanvasGroup>();
    }

    private void BackLevel()
    {
        Close();
        StartCoroutine(GameManager.Instance.BackLevel());
    }

    public void Open()
    {
        if (_canvsGourp.interactable)
            return;
        _canvsGourp.DOFade(1, 0.4f).onComplete += () => { _canvsGourp.interactable = true; Time.timeScale = 0; };
        _canvsGourp.blocksRaycasts = true;
    }
    public void Close()
    {
        if (!_canvsGourp.interactable)
            return;
        Time.timeScale = 1;
        _canvsGourp.DOFade(0, 0.2f).onComplete += () => { _canvsGourp.interactable = false; };
        _canvsGourp.blocksRaycasts = false;
        onStop?.Invoke();
    }
}
