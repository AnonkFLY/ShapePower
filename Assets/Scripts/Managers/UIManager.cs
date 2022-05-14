using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public static readonly int close = Animator.StringToHash("Close");
    public static readonly int open = Animator.StringToHash("Open");
    public static readonly int normal = Animator.StringToHash("Normal");
    private UIAssets _uiAssets;
    private Transform _canvesTrans;
    private List<UIBase> _uiLiist = new List<UIBase>();
    private Dictionary<UIType, UIBase> _instanceUI = new Dictionary<UIType, UIBase>();

    public UIManager(UIAssets uIAssets)
    {
        _uiAssets = uIAssets;
        _canvesTrans = GameObject.FindObjectOfType<Canvas>().transform;
    }
    public void OpenUI(UIType type, bool closeLast = false)
    {
        var panel = GetUI(type);
        if (closeLast)
            CloseUI();
        if (panel.isOpen)
            return;
        panel.Open();
        panel.isOpen = true;
        IUpdatable update = panel.GetComponent<IUpdatable>();
        if (update != null)
            GameManager.Instance.onUIUpdate = new System.Action(update.OnUpdateView);
        else
            GameManager.Instance.onUIUpdate = null;
        panel.index = _uiLiist.Count;
        _uiLiist.Add(panel);
    }
    public void CloseUI()
    {
        CloseUI(_uiLiist.Count - 1);
    }
    public void CloseUI(int index)
    {
        DebugLog.Message($"The Close UI on {index}");
        var panel = _uiLiist[index];
        if (!panel.isOpen)
            return;
        panel.Close();
        panel.isOpen = false;
        _uiLiist.RemoveAt(index);
    }
    private UIBase GetUI(UIType type)
    {
        UIBase panel;
        if (!_instanceUI.TryGetValue(type, out panel))
        {
            panel = InstanceUIPanel(type);
            _instanceUI.Add(type, panel);
        }
        return panel;
    }
    private T GetUI<T>(UIType type) where T : UIBase
    {
        _instanceUI.TryGetValue(type, out var panel);
        return panel as T;
    }
    private UIBase InstanceUIPanel(UIType uIType)
    {
        //总之就是获取GameObject实例化然后获取UIBase组件
        var result = GameObject.Instantiate(_uiAssets.GetUIObj(uIType), _canvesTrans).GetComponent<UIBase>();
        result.RegisterUI(this);
        return result;
    }
}
public enum UIType
{
    StartView,
    LevelView,
    TransitionsView
}