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
    public SceneManager sceneManager;
    public PlayerManager playerManager;
    public InputManager inputManager = new InputManager();

    [Header("Requirement")]
    [SerializeField] private UIAssets _uiAssets;
    [SerializeField] private UIType _startOpen;
    //事件
    public Action onUIUpdate;
    [SerializeField]
    private RoleBase _currentRole;
    private MoneyView _moneyView;
    private PlayerController _playerController;
    private void Awake()
    {
        SingleInit();
        ManangerInit();
        Application.targetFrameRate = 60;
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
    public void UnLockNextLevel()
    {
        var i = archiveManager.UnLockNextLevel();
        if (i == -1)
            return;
        sceneManager.SetUnlocked(i);
    }
    private void Start()
    {
        uiManager.OpenUI(_startOpen);
    }
    private void Update()
    {
        onUIUpdate?.Invoke();
        if (Input.GetKeyDown(KeyCode.N))
        {
            UnLockNextLevel();
        }
    }

    public void OnRoleChange(RoleBase choose, int index)
    {
        print($"On Choose {choose.name}");
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
        //DontDestroyOnLoad(gameObject);
    }
    public IEnumerator BackLevel()
    {
        uiManager.OpenUI(UIType.TransitionsView, true);
        yield return new WaitForSeconds(1.4f);
        if (sceneObj)
        {
            Destroy(sceneObj);
            Resources.UnloadUnusedAssets();
        }
        if (_playerController)
        {
            Destroy(_playerController.gameObject);
        }
        uiManager.OpenUI(UIType.LevelView, true);
    }

    private GameObject sceneObj;
    public void LoadScene(string name)
    {
        uiManager.OpenUI(UIType.TransitionsView, true);
        var transition = uiManager.GetUI<TransitionsView>(UIType.TransitionsView);
        sceneManager.LoadAsync(name, obj =>
        {
            uiManager.CloseUI();
            uiManager.OpenUI(UIType.GameView);
            sceneObj = obj;
            _playerController = playerManager.CreatePlayer(_currentRole);
        });
    }
}
