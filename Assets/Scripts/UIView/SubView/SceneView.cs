using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneView : MonoBehaviour
{
    private BuyButton _lockButton;
    private SceneData _data;
    private Button _enterButton;
    private ShapePowerArchive _archive;
    public void InitSceneView(SceneData data, int index)
    {
        _data = data;
        _archive = GameManager.Instance.archiveManager;
        _lockButton = GetComponentInChildren<BuyButton>();
        _enterButton = GetComponentInChildren<Button>();
        _enterButton.onClick.AddListener(EnterScene);
        transform.GetChild(0).GetComponent<Image>().sprite = data.sceneSprite;
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
        DebugLog.Message($"进入场景{_data.sceneName}");
    }
}
