using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneView : MonoBehaviour, IJumpable
{
    private BuyButton _lockButton;
    private SceneData _data;
    private EventButton _enterButton;
    private ShapePowerArchive _archive;
    private Transform _transform;
    private Transform _body;
    private SceneManager _sceneManager;
    private bool isClose = true;
    public void InitSceneView(SceneData data, int index)
    {
        _data = data;
        _transform = transform;
        _body = _transform.GetChild(0);
        _transform.localScale = Vector3.zero;
        _archive = GameManager.Instance.archiveManager;
        _sceneManager = GameManager.Instance.sceneManager;
        _lockButton = GetComponentInChildren<BuyButton>();
        _enterButton = GetComponentInChildren<EventButton>();
        _enterButton.onClick += EnterScene;
        _enterButton.onDown += Enter;
        _enterButton.onExit += Exit;
        _body.GetChild(0).GetComponent<Image>().sprite = data.sceneSprite;
        if (data.IsUnlocked)
        {
            _lockButton.gameObject.SetActive(false);
        }
        else
        {
            _lockButton.onClick += () =>
            {
                _lockButton.BuyResult(false);
            };
            data.onUnLock += () =>
            {
                _lockButton.gameObject.SetActive(false);
            };
        }
    }

    private void EnterScene()
    {
        Jump();
        _body.DOScale(1, 0.2f);
        isClose = true;
        GameManager.Instance.LoadScene(_data.sceneName);
    }
    public void Jump()
    {
        if (!isClose)
            return;
        DebugLog.Message("Jump");
        _transform.DOScale(1, 0.6f);
        isClose = false;
    }
    public void Enter()
    {
        if (isClose)
            return;
        _body.DOScale(0.8f, 0.2f);
    }
    public void Exit()
    {
        if (isClose)
            return;
        _body.DOScale(1f, 0.15f);
    }
    public void WithDraw()
    {
        _transform.DOScale(0, 0.4f);
        isClose = true;
    }
}
