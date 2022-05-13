using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get => _instance; }
    //Managers
    public InputManager inputManager;
    public ArchiveManager<ShapePowerSave> archiveManager;
    public UIManager uiManager;

    [Header("Requirement")]
    [SerializeField] private UIAssets _uiAssets;
    [SerializeField] private UIType _startOpen;
    [SerializeField] private UIType[] _testUI;
    private void Awake()
    {
        SingleInit();
        ManangerInit();
    }
    private void Start()
    {

    }

    private void ManangerInit()
    {
        inputManager = new InputManager();
        archiveManager = new ArchiveManager<ShapePowerSave>();
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
