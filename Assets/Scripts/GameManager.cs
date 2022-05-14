using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get => _instance; }
    //Managers
    public ShapePowerArchive archiveManager;
    public UIManager uiManager;

    [Header("Requirement")]
    [SerializeField] private UIAssets _uiAssets;
    [SerializeField] private UIType _startOpen;
    //事件
    public Action<RoleBase,int> onRoleChange;
    public Action onUIUpdate;
    private RoleBase _currentRole;
    private MoneyView _moneyView;
    private void Awake()
    {
        SingleInit();
        ManangerInit();
        Application.targetFrameRate = 60;
        onRoleChange = new Action<RoleBase,int>(OnRoleChange);
        _moneyView = GetComponentInChildren<MoneyView>();

    }
    public void OpenMoneyView()
    {
        _moneyView.gameObject.SetActive(true);
        archiveManager.AddMoney(0);
    }
    public void CloseMoneyView()
    {
        _moneyView.gameObject.SetActive(false);
    }
    private void Start()
    {
        uiManager.OpenUI(_startOpen);
    }
    private void Update()
    {
        onUIUpdate?.Invoke();
    }
    private void OnRoleChange(RoleBase choose,int index)
    {
        _currentRole = choose;
        archiveManager.SetChoose(index);
    }

    private void ManangerInit()
    {
        archiveManager = new ShapePowerArchive();
        archiveManager.Load();
        uiManager = new UIManager(_uiAssets);
    }

    private void SingleInit()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

}
