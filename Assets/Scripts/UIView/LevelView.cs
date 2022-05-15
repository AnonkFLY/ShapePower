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
        roleChoices[_archive.archiveObj.choose].GetComponent<Toggle>().isOn = true;
        InitRoleData();
        InitLevelData();
    }

    private void InitRoleData()
    {
        for (int i = 0; i < roleChoices.Length; i++)
        {
            var role = _archive.archiveObj.roles[i];
            if (role != null)
            {
                role.sprite = datas[i].sprite;
                datas[i] = role;
            }
            var unLock = _archive.archiveObj.GetRoleIsPurchased(i);
            roleChoices[i].InitChoiceView(datas[i], i, unLock);
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
        uiManager.OpenUI(UIType.StartView, true);
    }
    public override void Close()
    {
        _panelAnimator?.Play(UIManager.close);
        CloseSubView();
    }
    public override void Open()
    {
        _panelAnimator?.Play(UIManager.open);
    }
    public void OpenSubView()
    {

        StartCoroutine(JumpUpRoleView(true));
    }
    public void CloseSubView()
    {
        StartCoroutine(JumpUpRoleView(false));
    }
    private IEnumerator JumpUpRoleView(bool open)
    {
        var wait = new WaitForSeconds(0.2f);
        foreach (var item in roleChoices)
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
