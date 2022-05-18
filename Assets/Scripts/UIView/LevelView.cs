using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : UIBase, IUpdatable
{
    [SerializeField]
    private RoleBase[] datas;
    private SceneData[] sceneDatas;
    private SceneView[] sceneViews;
    private Button _backButton;
    private Animator _panelAnimator;
    private RoleChoice[] roleChoices;
    private ShapePowerArchive _archive;

    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _panelAnimator = GetComponent<Animator>();
        _backButton = transform.Find("Back").GetComponent<Button>();
        _backButton.onClick.AddListener(Back);
        roleChoices = GetComponentsInChildren<RoleChoice>();
        sceneViews = GetComponentsInChildren<SceneView>();
        _archive = GameManager.Instance.archiveManager;
        sceneDatas = GameManager.Instance.sceneManager.scenesData;

        InitRoleData();
        InitLevelData();
        var chooseRole = roleChoices[_archive.archiveObj.choose];
        chooseRole.OnClick(true);
        chooseRole.GetComponent<Toggle>().isOn = true;
    }

    private void InitRoleData()
    {
        for (int i = 0; i < roleChoices.Length; i++)
        {
            // DebugLog.Warning($"尝试读取{i}");
            // if (_archive.archiveObj.roles != null && _archive.archiveObj.roles.Length > i)
            // {
            //     DebugLog.Warning($"读取{i}是否为空");
            //     var role = _archive.archiveObj.roles[i];
            //     if (role != null)
            //     {
            //         role.sprite = datas[i].sprite;
            //         datas[i] = role;
            //         DebugLog.Warning($"{i}不为空，写入");
            //     }
            // }
            if (_archive.archiveObj.roles != null)
            {
                if (_archive.archiveObj.roles[i] == null)
                {
                    _archive.archiveObj.roles[i] = datas[i];
                }
                else
                {
                    datas[i] = _archive.archiveObj.roles[i];
                }
            }
            var unLock = _archive.archiveObj.GetRoleIsPurchased(i);
            roleChoices[i].InitChoiceView(datas[i], i, unLock);
            _archive.InitRoleData(datas);
        }
    }
    private void InitLevelData()
    {
        for (int i = 0; i < sceneDatas.Length; i++)
        {
            var unLock = _archive.archiveObj.GetLevelLocked(i);
            sceneDatas[i].IsUnlocked = unLock;
            sceneViews[i].InitSceneView(sceneDatas[i], i);
        }
    }

    private void Back()
    {
        AudioManager.Instance.PlaySoundEffect(8);
        uiManager.OpenUI(UIType.StartView, true);
    }
    public override void Close()
    {
        _panelAnimator?.Play(UIManager.close);
        CloseSubView();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public override void Open()
    {
        _panelAnimator?.Play(UIManager.open);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public void OpenSubView()
    {
        StartCoroutine(JumpUpRoleView<RoleChoice>(true, 0.15f, roleChoices));
        StartCoroutine(JumpUpRoleView<SceneView>(true, 0.15f, sceneViews));
    }
    public void CloseSubView()
    {
        StartCoroutine(JumpUpRoleView<RoleChoice>(false, 0.1f, roleChoices));
        StartCoroutine(JumpUpRoleView<SceneView>(false, 0.05f, sceneViews));
    }
    private IEnumerator JumpUpRoleView<T>(bool open, float timer, T[] array) where T : IJumpable
    {
        var wait = new WaitForSeconds(timer);
        foreach (var item in array)
        {
            if (open)
                item.Jump();
            else
                item.WithDraw();
            yield return wait;
        }

    }

    public void OnUpdateView()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.OpenUI(UIType.StartView, true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.archiveManager.AddMoney(100);
        }
    }
}
